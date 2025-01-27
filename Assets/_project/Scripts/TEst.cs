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
        EditorGUI.BeginProperty(position, label, property);
        // _bool = EditorGUI.BeginFoldoutHeaderGroup(position, _bool, property.displayName);
        position.height = EditorGUIUtility.singleLineHeight;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
        SerializedProperty year = property.FindPropertyRelative("year");
        SerializedProperty month = property.FindPropertyRelative("month");
        SerializedProperty day = property.FindPropertyRelative("day");
        
        SerializedProperty hour = property.FindPropertyRelative("hour");
        SerializedProperty minute = property.FindPropertyRelative("minute");
        SerializedProperty second = property.FindPropertyRelative("second");

        float width = position.width / 6f;
        Rect dateRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, width, EditorGUIUtility.singleLineHeight);
        
        if (property.isExpanded)
        {
            // EditorGUI.BeginProperty(dateRect, new GUIContent(year.displayName), year);
            // EditorGUI.PropertyField(dateRect, year, new GUIContent(year.displayName));
            EditorGUI.LabelField(dateRect, new GUIContent(year.displayName));
            dateRect.x += width;
            year.intValue = EditorGUI.IntField(dateRect, year.intValue);
            dateRect.x += width;
            
            EditorGUI.LabelField(dateRect, new GUIContent(month.displayName));
            dateRect.x += width;
            month.intValue = EditorGUI.IntField(dateRect, month.intValue);
            dateRect.x += width;
            
            EditorGUI.LabelField(dateRect, new GUIContent(day.displayName));
            dateRect.x += width;
            day.intValue = EditorGUI.IntField(dateRect, day.intValue);
            dateRect.x += width;
            
            dateRect.x = position.x;
            dateRect.y += EditorGUIUtility.singleLineHeight;
            
            EditorGUI.LabelField(dateRect, new GUIContent(hour.displayName));
            dateRect.x += width;
            hour.intValue = EditorGUI.IntField(dateRect, hour.intValue);
            dateRect.x += width;
            
            EditorGUI.LabelField(dateRect, new GUIContent(minute.displayName));
            dateRect.x += width;
            minute.intValue = EditorGUI.IntField(dateRect, minute.intValue);
            dateRect.x += width;
            
            EditorGUI.LabelField(dateRect, new GUIContent(second.displayName));
            dateRect.x += width;
            second.intValue = EditorGUI.IntField(dateRect, second.intValue);
            dateRect.x += width;
            // EditorGUI.EndProperty();
        }
        
        EditorGUI.EndProperty();
        
        // base.OnGUI(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return property.isExpanded ? EditorGUIUtility.singleLineHeight * 3 : EditorGUIUtility.singleLineHeight;
    }
}
    
#endif