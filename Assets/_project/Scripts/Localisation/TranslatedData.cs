using System;
using UnityEngine;

namespace _project.Scripts.Localisation
{
    [Serializable]
    public class TranslatedData
    {
        [SerializeField] protected Loc _loc = Loc.FR_fr;
        [SerializeField, TextArea] protected string _content;
        
        public Loc loc => _loc;

        public string content => _content;
    }
    
    [Serializable]
    public class TranslatedDialogData : TranslatedData
    {
       
        [SerializeField] private AudioClip _audioClip;
        
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