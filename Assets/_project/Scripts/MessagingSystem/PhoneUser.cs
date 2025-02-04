using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    [CreateAssetMenu(fileName = "PhoneUser0", menuName = "MessagingSystem/Phone User"), Manageable, Editable]
    public class PhoneUser : ScriptableObject
    {
        [SerializeField] private string _userId;
        [SerializeField] private Sprite _icon;
        [SerializeField] private List<TranslatedData> _names;

        public Sprite Icon => _icon;
        public string UserId => _userId;
        
        public string GetNameFor(Loc loc)
        {
            return _names.Find(e => e.loc == loc).content ?? throw new NullReferenceException($"No loc '{loc}' was found for user '{_userId}'");
        }
    }
}