using System;
using UnityEngine;

#if UNITY_EDITOR
using _project.Scripts;
using UnityEditor;
#endif

namespace _project.Scripts
{
    public class TEst : MonoBehaviour
    {
        [SerializeField] private FDateTime time;
    }

    [Serializable]
    public class FDateTime
    {
        [SerializeField] private int year;
        [SerializeField, Range(1, 12)] private int month;
        [SerializeField, Range(1, 31)] private int day;
        [SerializeField, Range(0, 23)] private int hour;
        [SerializeField, Range(0, 59)] private int minute;
        [SerializeField, Range(0, 59)] private int second;
        
        
        public DateTime Get()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
    }
    
    
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(FDateTime))]
public class DateTimeDrawer : PropertyDrawer
{
    private bool _bool;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _bool = EditorGUI.BeginFoldoutHeaderGroup(position, _bool, property.displayName);
        SerializedProperty year = property.FindPropertyRelative("year");

        float width = position.width / 3f;
        Rect dateRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, width, EditorGUIUtility.singleLineHeight);

        if (_bool)
        {
            EditorGUI.PropertyField(dateRect, year, new GUIContent(year.displayName));
        }
        
        // base.OnGUI(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _bool ? EditorGUIUtility.singleLineHeight * 3 : EditorGUIUtility.singleLineHeight;
    }
}
    
#endif