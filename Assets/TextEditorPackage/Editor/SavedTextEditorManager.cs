using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SavedTextEditorManager
{
    public static void SaveText(string fileName, List<StylizedChar> stylizedText)
    {
        SavedTextSO savedTextSo = ScriptableObject.CreateInstance<SavedTextSO>();
        savedTextSo.SetupValues(fileName, stylizedText);
        //TODO might need to change that Element
        string path = "Assets/TextEditorPackage/CreatedTextSO/Resources/" + fileName + ".asset";
        AssetDatabase.CreateAsset(savedTextSo, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeObject = savedTextSo;
        EditorUtility.FocusProjectWindow();
    }
    
    public static void ModifyText(string fileName, List<StylizedChar> stylizedText)
    {
        AssetDatabase.DeleteAsset("Assets/TextEditorPackage/CreatedTextSO/Resources/" + fileName + ".asset");
        SaveText(fileName, stylizedText);
        /*
        SavedTextSO savedTextSo = SavedTextManager.LoadText(fileName);
        savedTextSo.SetupValues(fileName, stylizedText);
        Selection.activeObject = savedTextSo;
        EditorUtility.FocusProjectWindow();*/
    }
}
