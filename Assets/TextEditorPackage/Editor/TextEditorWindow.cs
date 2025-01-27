using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class TextEditorWindow : EditorWindow
{
    
    private TextField _textField;
    private Label _labelField;
    
    private string _selectedText = "";
    private int[] _selectedTextIndexes;
    private int[] _lastSelectionIndexes = {-1, -1}; 
    private bool _markSelectedText = true;
    
    private TextData _textData = new(false, false, false,false, new Marker(false, new Color(0,0,0,60)), Color.black, 12);
    
    private StylizedCharList _stylizedTextManager;
    
    //Saves
    private string _saveFileName = "";
    private string _saveActionResult = "";
    
    //private Texture2D _texture;

    [MenuItem("Window/Text Editor")]
    public static void ShowWindow()
    {
        GetWindow<TextEditorWindow>("Text Editor");
    }

    void CreateGUI()
    {
        //TextField Creation
        _textField = new TextField();
        rootVisualElement.Add(_textField);
        _textField.multiline = true;
        _textField.maxLength = 1000;
        _textField.style.minHeight = 20;
        _textField.style.top = 30;
        _textField.style.maxHeight = 100;
        
        //LabelField Creation
        _labelField = new Label();
        rootVisualElement.Add(_labelField);
        _labelField.selection.isSelectable = true;
        _labelField.enableRichText = true;
        _labelField.style.minHeight = 20;
        _labelField.style.top = 40;
        _labelField.style.paddingLeft = 10; _labelField.style.paddingRight = 10;
        
        //Creating Stylized TextManager
        
        _stylizedTextManager = new StylizedCharList();
        _selectedTextIndexes = new[] {-1,-1};
    }
    
    void OnGUI()
    {
        TextData textDataCopy = _textData.Clone();
        //Check if textField and labelField is null
        if (_textField == null || _labelField == null) return;
        
        //Style with richText on 
        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            richText = true
        };
        
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("<b><size=20>Text Editor</size></b>", style);
        EditorGUILayout.Space(_textField.resolvedStyle.height + _labelField.resolvedStyle.height + 20);
        
        _textField.RegisterValueChangedCallback(evt =>
        {
            _stylizedTextManager.CheckStylizedText(_textField.value,_textData);
            _selectedText = "";
            _selectedTextIndexes = new int[] {-1, -1};
            if (_saveActionResult != "<color=green><b>Text Successfully Imported !</b></color>") _saveActionResult = "";
        });
       
        #region --- Selection Text Fields ----
        
        //Select Text Part Button
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Selection : " + _selectedText, style);
        
        
        if (GUILayout.Button("Select Text"))
        {
            //int[] currentSelectionIndexes = GetSelectionIndexes(_textField, _labelField);
            _selectedText = GetSelectedText(_textField.text, _lastSelectionIndexes);
            _selectedTextIndexes = _lastSelectionIndexes;
            
            //TODO: maybe change that to make it copy most used for each TextData element
            _textData = _stylizedTextManager.StylizedText[_selectedTextIndexes[0]].TextData.Clone();
        }
        else
        {
            int[] currentSelectionIndexes = GetSelectionIndexes(_textField, _labelField);
            if (currentSelectionIndexes[0] != currentSelectionIndexes[1]) _lastSelectionIndexes = currentSelectionIndexes;
        }
        
        if (GUILayout.Button("Deselect"))
        {
            _selectedText = "";
            _selectedTextIndexes = new int[] { -1, -1 };
        }
        
        EditorGUILayout.EndHorizontal();
        
        _markSelectedText = EditorGUILayout.ToggleLeft("Mark Selected Text : ",_markSelectedText);
        
        #endregion
        
        EditorGUILayout.Space(5);
        
        #region --- TextData Fields ---
        
        //Toggle Buttons for Bold, Italic and Underline
        EditorGUILayout.BeginHorizontal();
        
        _textData.IsBold = EditorGUILayout.ToggleLeft("Bold",_textData.IsBold, GUILayout.Width(100));
        _textData.IsItalic = EditorGUILayout.ToggleLeft("Italic",_textData.IsItalic,GUILayout.Width(100));
        _textData.IsUnderline = EditorGUILayout.ToggleLeft("Underline",_textData.IsUnderline,GUILayout.Width(100));
        _textData.IsStrikeThrough = EditorGUILayout.ToggleLeft("StrikeThrough",_textData.IsStrikeThrough,GUILayout.Width(100));

        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(5);
        
        //Color Field
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Color of the Font : <color=#" + _textData.Color.ToHexString() + ">Example</color>", style);
        _textData.Color = EditorGUILayout.ColorField("",  _textData.Color,GUILayout.Width(300));
        if (GUILayout.Button("Apply"))
        {
            _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes, _textData,
                TextDataType.Color);
        }
        
        EditorGUILayout.EndHorizontal();
        
        //Marker Field
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Mark Text ", style, GUILayout.Width(100));
        _textData.Marker.IsMarked = EditorGUILayout.ToggleLeft("", _textData.Marker.IsMarked, GUILayout.Width(100));
        _textData.Marker.Color = EditorGUILayout.ColorField(new GUIContent(""),  _textData.Marker.Color, true, true,false,GUILayout.Width(300));
        if (GUILayout.Button("Apply"))
        {
            _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes, _textData,
                TextDataType.Marker);
        }
        
        EditorGUILayout.EndHorizontal();

        
        //Size Field
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Size of the Font : <size=" + _textData.Size + ">Example</size>", style, new [] {GUILayout.Width(250), GUILayout.MaxHeight(25) });
        _textData.Size = EditorGUILayout.IntSlider(_textData.Size, 3, 30);
        if (GUILayout.Button("Apply"))
        {
            _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes, _textData,
                TextDataType.Size);
        }
        
        EditorGUILayout.EndHorizontal();
        
        //Apply Changed Files

        if (_selectedText != "")
        {
            if (_textData.IsBold != textDataCopy.IsBold) 
            { _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes,_textData,TextDataType.Bold); }
            if (_textData.IsItalic != textDataCopy.IsItalic) 
            { _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes,_textData,TextDataType.Italic); }
            if (_textData.IsUnderline != textDataCopy.IsUnderline) 
            { _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes,_textData,TextDataType.Underline); }
            if (_textData.IsStrikeThrough != textDataCopy.IsStrikeThrough) 
            { _stylizedTextManager.ApplySpecificDataToSelectionIndexes(_selectedTextIndexes,_textData,TextDataType.StrikeThrough); }
        }
        #endregion
        
        _labelField.text = _stylizedTextManager.GetStringStylizedText(_markSelectedText? _selectedTextIndexes : new int[] {-1, -1});
        
        GUILayout.Space(20);
        
        #region --- Saving Text Section ---
        
        _saveFileName = EditorGUILayout.TextField("File Name :",_saveFileName);
        
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Text To File"))
        {
            _saveActionResult = "";
            if (_saveFileName != "")
            {
                if (SavedTextManager.DoesTextExist(_saveFileName))
                {
                    _saveActionResult = "<color=red><b>File Name already taken !</b></color>";
                }
                else
                {
                    SavedTextEditorManager.SaveText(_saveFileName, _stylizedTextManager.GetStylizedTextCopy());
                    _saveActionResult = "<color=green><b>Text Successfully Saved !</b></color>";
                }
            }
        }
        if (GUILayout.Button("Modify File"))
        {
            _saveActionResult = "";
            if (_saveFileName != "")
            {
                if (!SavedTextManager.DoesTextExist(_saveFileName))
                {
                    _saveActionResult = "<color=red><b>File does not exist !</b></color>";
                }
                else
                {
                    SavedTextEditorManager.ModifyText(_saveFileName, _stylizedTextManager.GetStylizedTextCopy());
                    _saveActionResult = "<color=green><b>Text Successfully Modified !</b></color>";
                }
            }
        }
        if (GUILayout.Button("Import File"))
        {
            _saveActionResult = "";
            if (_saveFileName != "")
            {
                if (!SavedTextManager.DoesTextExist(_saveFileName))
                {
                    _saveActionResult = "<color=red><b>File does not exist !</b></color>";
                }
                else
                {
                    SavedTextSO stylizedTextSo = SavedTextManager.LoadText(_saveFileName);
                    _stylizedTextManager.StylizedText = stylizedTextSo.GetStylizedTextCopy();
                    _textField.value = _stylizedTextManager.GetStringNotStylizedText();
                    _labelField.text = _stylizedTextManager.GetStringStylizedText();
                    _saveActionResult = "<color=green><b>Text Successfully Imported !</b></color>";
                }
            }
        }
        
        GUILayout.EndHorizontal();
        
        GUILayout.Label(_saveActionResult, style);
        
        #endregion
        
        /*_texture = (Texture2D)EditorGUILayout.ObjectField("texture ", _texture, typeof(Texture2D), true);
        _labelField.style.backgroundImage = _texture;*/
        
        //EditorGUILayout.LabelField("Selected Text : " + selectedText);
    }

    public void LoadText(SavedTextSO savedTextSo)
    {
        _stylizedTextManager.StylizedText = savedTextSo.GetStylizedTextCopy();
        _textField.value = _stylizedTextManager.GetStringNotStylizedText();
        _labelField.text = _stylizedTextManager.GetStringStylizedText();
        _saveActionResult = "<color=green><b>Text Successfully Imported !</b></color>";
    }

    private int[] GetSelectionIndexes(TextField textField)
    {
        int[] selectionIndexes = new int[2];
        selectionIndexes[0] = textField.selectIndex < textField.cursorIndex? textField.selectIndex : textField.cursorIndex;
        selectionIndexes[1] = textField.selectIndex < textField.cursorIndex? textField.cursorIndex : textField.selectIndex;
        return selectionIndexes;
    }

    private int[] GetSelectionIndexes(TextField textField, Label label)
    {
        int[] selectionIndexes = GetSelectionIndexes(textField);
        if (selectionIndexes[0] == selectionIndexes[1])
        {
            selectionIndexes[0] = label.selection.selectIndex < label.selection.cursorIndex? label.selection.selectIndex : label.selection.cursorIndex;
            selectionIndexes[1] = label.selection.selectIndex < label.selection.cursorIndex? label.selection.cursorIndex : label.selection.selectIndex;
        }
        return selectionIndexes;
    }

    private string GetSelectedText(string text, int[] selectionIndexes)
    {
        if (text.Length < selectionIndexes[1])
        {
            return "";
        }
        string selectedText = "";
        
        selectedText = text.Substring(selectionIndexes[0], selectionIndexes[1] - selectionIndexes[0]);
        
        
        return selectedText;
    }

    //Return a list of three string : Begin Text - Selected Text - End Text
    private string[] GetSubstringfromSelectedIndex(string text, int[] selectionIndexes)
    {
        string[] textList = new string[3];
        textList[1] = GetSelectedText(text, selectionIndexes);

        textList[0] = text.Substring(0, selectionIndexes[0]);
        textList[2] = text.Substring(selectionIndexes[1], text.Length - textList[0].Length - textList[1].Length);
        
        return textList;
    }
}
