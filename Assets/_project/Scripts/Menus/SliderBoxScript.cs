using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class SliderBoxScript : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private List<TranslatedData> _translatedData;

        public void Init(float value)
        {
            SetValue(value);
            GameSettings.OnLocChanged += OnGameSettingsLocChanged;
            _text.text = GetContentFor(GameSettings.Instance.loc);
        }

        private void OnGameSettingsLocChanged(Loc loc)
        {
            _text.text = GetContentFor(loc);
        }
        
        public void SetValue(float value)
        {
            _slider.value = value;
        }
        
        public void AddListener(UnityAction<float> listener)
        {
            _slider.onValueChanged.AddListener(listener);
        }
        
        private string GetContentFor(Loc loc)
        {
            return _translatedData.Find(e => e.loc == loc).content ?? throw new NullReferenceException($"No loc '{loc}' was found for message from '{gameObject.name}'");
        }

        public void ClearListener()
        {
            GameSettings.OnLocChanged -= OnGameSettingsLocChanged;
        }

        public float GetValue()
        {
            return _slider.value;
        }

        public float GetMixerValue()
        {
            return -80 + GetValue() * 80;
        }
    }
}