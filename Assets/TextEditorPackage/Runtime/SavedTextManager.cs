using UnityEngine;

public static class SavedTextManager
{
    public static SavedTextSO LoadText(string fileName)
    {
        return Resources.Load<SavedTextSO>(fileName);
        //return AssetDatabase.LoadAssetAtPath<SavedTextSO>("Assets/TextEditorPackage/CreatedTextSO/" + fileName + ".asset");
    }

    public static bool DoesTextExist(string fileName)
    {
        return Resources.Load<SavedTextSO>(fileName) != null;
        //return AssetDatabase.LoadAssetAtPath<SavedTextSO>("Assets/TextEditorPackage/CreatedTextSO/Resources/" + fileName + ".asset") != null;
    }
}
