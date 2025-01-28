using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _project.Scripts.MessagingSystem
{
    [Serializable]
    public class FDateTime
    {
        [SerializeField] private int _year;
        [SerializeField, Range(1, 12)] private int _month;
        [SerializeField, Range(1, 31)] private int _day;
        [SerializeField, Range(0, 23)] private int _hour;
        [SerializeField, Range(0, 59)] private int _minute;
        [SerializeField, Range(0, 59)] private int _second;
        
        
        public int year => _year;
        public int month => _month;
        public int day => _day;
        public int hour => _hour;
        public int minute => _minute;
        public int second => _second; 


        public FDateTime(FDateTime fDateTime)
        {
            _year = fDateTime.year;
            _month = fDateTime.month;
            _day = fDateTime.day;
            _hour = fDateTime.hour;
            _minute = fDateTime.minute;
            _second = fDateTime.second;
        }
        
        public DateTime Get()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public FDateTime AddSeconds(int seconds)
        {
            int addedMin = (second + seconds) / 60;
            _second = (second + seconds) % 60;
            
            int addedHour = (minute + addedMin) / 60;
            _minute = (minute + addedMin) % 60;
            
            int addedDay = (hour + addedHour) / 24;
            _hour = (hour + addedHour) % 24;

            int addedMonth = (day + addedDay) / 31;
            _day = (day + addedDay) % 31 + 1;
            
            int addedYear = (month + addedMonth) / 12;
            _month = (month + addedMonth) % 12 + 1;

            _year = year + addedYear;
            return this;
        }

        public override string ToString()
        {
            return "FDateTime(d:"+day+", m:"+month+", y:"+year+", h:"+hour+", m:"+minute+", s:"+second+")";
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
            SerializedProperty year = property.FindPropertyRelative("_year");
            SerializedProperty month = property.FindPropertyRelative("_month");
            SerializedProperty day = property.FindPropertyRelative("_day");
            
            SerializedProperty hour = property.FindPropertyRelative("_hour");
            SerializedProperty minute = property.FindPropertyRelative("_minute");
            SerializedProperty second = property.FindPropertyRelative("_second");

            float width = position.width / 6f;
            Rect dateRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, width, EditorGUIUtility.singleLineHeight);
            
            if (property.isExpanded)
            {
                // EditorGUI.BeginProperty(dateRect, new GUIContent(year.displayName), year);
                // EditorGUI.PropertyField(dateRect, year, new GUIContent(year.displayName));
                EditorGUI.LabelField(dateRect, new GUIContent(year.displayName));
                dateRect.x += width;
                year.intValue = Math.Clamp(EditorGUI.IntField(dateRect, year.intValue), 0, 9999);
                dateRect.x += width;
                
                EditorGUI.LabelField(dateRect, new GUIContent(month.displayName));
                dateRect.x += width;
                
                month.intValue = Math.Clamp(EditorGUI.IntField(dateRect, month.intValue), 1, 12);
                dateRect.x += width;
                
                EditorGUI.LabelField(dateRect, new GUIContent(day.displayName));
                dateRect.x += width;
                day.intValue = Math.Clamp(EditorGUI.IntField(dateRect, day.intValue), 1, 31);
                dateRect.x += width;
                
                dateRect.x = position.x;
                dateRect.y += EditorGUIUtility.singleLineHeight;
                
                EditorGUI.LabelField(dateRect, new GUIContent(hour.displayName));
                dateRect.x += width;
                hour.intValue = Math.Clamp(EditorGUI.IntField(dateRect, hour.intValue), 0, 23);
                dateRect.x += width;
                
                EditorGUI.LabelField(dateRect, new GUIContent(minute.displayName));
                dateRect.x += width;
                minute.intValue = Math.Clamp(EditorGUI.IntField(dateRect, minute.intValue), 0, 59);
                dateRect.x += width;
                
                EditorGUI.LabelField(dateRect, new GUIContent(second.displayName));
                dateRect.x += width;
                second.intValue = Math.Clamp(EditorGUI.IntField(dateRect, second.intValue), 0, 59);
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
}


