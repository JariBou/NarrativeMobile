using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class TranslatedButtonScript : MonoBehaviour
    {
        
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private List<TranslatedData> _translations;

        private void Awake()
        {
            _text.text = GetTextForLoc(GameSettings.Instance.loc);
            GameSettings.OnLocChanged += GameSettingsLocChanged;
        }

        private void OnDestroy()
        {
            GameSettings.OnLocChanged -= GameSettingsLocChanged;
        }

        private void GameSettingsLocChanged(Loc obj)
        {
            Debug.Log("Loc changed");
            _text.text = GetTextForLoc(obj);
        }

        public void AddOnClick(UnityAction onClickAction)
        {
            _button.onClick.AddListener(onClickAction);
        }
        
        private string GetTextForLoc(Loc loc)
        {
            return _translations.Find(e => e.loc == loc).content;
        }

        
    }
}