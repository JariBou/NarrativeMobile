using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIGenerator))]
public class UIGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var uiGenerator = target as UIGenerator;
        if (!uiGenerator) return;
        GUILayout.Label("--- This Script needs to be placed on a Canvas ---");
        
        GUILayout.Space(10);
        
        GUILayout.BeginHorizontal();
        
        GUILayout.Label("Saved Text File Name");
        uiGenerator._savedTextFileName = GUILayout.TextField(uiGenerator._savedTextFileName);
        
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);

        uiGenerator._backgroundSprite = (Sprite)EditorGUILayout.ObjectField("Background Sprite", uiGenerator._backgroundSprite, typeof(Sprite), false);
        //uiGenerator._autoSizeBackgroundSprite = GUILayout.Toggle(uiGenerator._autoSizeBackgroundSprite, "AutoSize Background Sprite");
       
        uiGenerator._backgroundSize = EditorGUILayout.Vector3Field("Background Size : ", uiGenerator._backgroundSize);
        //if(!uiGenerator._autoSizeBackgroundSprite)

        if (GUILayout.Button("Generate UI Element"))
        {
            uiGenerator.GenerateUIElement();
        }

    }
}

