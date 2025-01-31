using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.DialogSystem
{
    [CreateAssetMenu(fileName = "DialogInfo", menuName = "DialogSystem/DialogInfo")] [Manageable, Editable]
    public class DialogInfo : ScriptableObject
    {
        [SerializeField] private string _dialogName;
        [FormerlySerializedAs("_dialogs")] [SerializeField] private List<TranslatedDialogData> _localisations = new();

        public string dialogName => _dialogName;

        public TranslatedDialogData GetDialogForLoc(Loc loc)
        {
            return _localisations.Find(e => e.loc == loc) ?? throw new NullReferenceException($"No loc '{loc}' was found for dialog '{dialogName}'");
        }
    }

    [Serializable]
    public class TranslatedDialogData
    {
        [SerializeField] private Loc _loc = Loc.FR_fr;
        [SerializeField] private string _content;
        [SerializeField] private AudioClip _audioClip;

        public Loc loc => _loc;

        public string content => _content;

        public AudioClip audioClip => _audioClip;

        public float GetClipDuration()
        {
            return audioClip != null ? audioClip.length : 2;
        }
    }

    public enum Loc
    {
        FR_fr, 
        EN_en
    }
}