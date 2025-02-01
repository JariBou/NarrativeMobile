using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using NUnit.Framework;
using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    [CreateAssetMenu(fileName = "Message0", menuName = "MessagingSystem/Message"), Manageable, Editable]
    public class Message : ScriptableObject
    {
        [SerializeField] private FDateTime _sentDateTime;
        [SerializeField] private List<TranslatedData> _content;
        [SerializeField] private PhoneUser _user;

        public PhoneUser User => _user;
        public List<TranslatedData> Content => _content;
        public FDateTime SentDateTime => _sentDateTime;

        public string GetContentFor(Loc loc)
        {
            return Content.Find(e => e.loc == loc).content ?? throw new NullReferenceException($"No loc '{loc}' was found for message from '{User.name}'");
        }
    }
}