using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(SavedTextSO))]
public class SavedTextSOEditor : Editor
{
    private Label _fileNameField;
    private Label _stylizedTextField;
    private VisualElement _root;
    private Button _buttonOpen;
    private Button _buttonCopy;

    public override VisualElement CreateInspectorGUI()
    {
        _root = new VisualElement();

        _fileNameField = new Label();
        _stylizedTextField = new Label();
        _root.Add(_fileNameField);
        _root.Add(_stylizedTextField);
        _fileNameField.enableRichText = true;
        _fileNameField.style.minHeight = 20;
        _fileNameField.style.top = 10;
        _fileNameField.style.paddingLeft = 10; _fileNameField.style.paddingRight = 10;
        _fileNameField.text = "ERROR";
        
        _stylizedTextField.style.minHeight = 20;
        _stylizedTextField.style.top = 40;
        _stylizedTextField.style.paddingLeft = 10; _stylizedTextField.style.paddingRight = 10;

            
        var savedTextSo = target as SavedTextSO;
        if (!savedTextSo) return _root;
        
        _fileNameField.text = "<size=20><b>File Name</b> : <i>" + savedTextSo.GetFileName() + "</i></size>";
        
        _stylizedTextField.text = "Stylized Text : \n \n" + savedTextSo.GetTextRichFormat();
        
        _buttonOpen = new Button();
        _root.Add(_buttonOpen);
        _buttonOpen.style.top = 50;
        _buttonOpen.text = "Open";
        _buttonOpen.clicked+= () =>
        {
            TextEditorWindow window = (TextEditorWindow) EditorWindow.GetWindow( typeof(TextEditorWindow), false, "TextEditor Window" );
            window.LoadText(savedTextSo);
            window.Show();
        };
        _buttonCopy = new Button();
        _root.Add(_buttonCopy);
        _buttonCopy.style.top = 50;
        _buttonCopy.text = "Copy RichTextFormat to clipboard";
        _buttonCopy.clicked += () =>
        {
            EditorGUIUtility.systemCopyBuffer = savedTextSo.GetTextRichFormat();
        };
        
        return _root;
    }
}
