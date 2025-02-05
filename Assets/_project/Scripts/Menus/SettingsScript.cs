using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class SettingsScript : MonoBehaviour
    {
        [SerializeField] private SliderBoxScript _masterSliderBox;
        [SerializeField] private SliderBoxScript _sfxSliderBox;
        [SerializeField] private SliderBoxScript _voicesSliderBox;
        [SerializeField] private TMP_Dropdown _languageDropdown;
        [SerializeField] private Button _backButton;
        [SerializeField] private TranslatedButtonScript _backButtonScript;
        [SerializeField] private MainMenuScript _menuScript;
        
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private List<TranslatedData> _titleTranslatedData = new();
        
        [SerializeField]
        private AudioMixer Mixer;

        private void Awake()
        {
            _masterSliderBox.Init(PlayerPrefs.GetFloat("volume_master"));
            float val = -80 + _masterSliderBox.GetValue() * 80;
            Mixer.SetFloat("volume_master", val);
            
            _sfxSliderBox.Init(PlayerPrefs.GetFloat("volume_sfx"));
            val = -80 + _sfxSliderBox.GetValue() * 80;
            Mixer.SetFloat("volume_sfx", val);
            
            _voicesSliderBox.Init(PlayerPrefs.GetFloat("volume_voices"));
            val = -80 + _voicesSliderBox.GetValue() * 80;
            Mixer.SetFloat("volume_voices", val);
            
            _masterSliderBox.AddListener(MasterSliderValueChanged);
            _sfxSliderBox.AddListener(SfxSliderValueChanged);
            _voicesSliderBox.AddListener(VoicesSliderValueChanged);
            
            _languageDropdown.options.Clear();
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData("French"));
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData("English"));
            _languageDropdown.value = (int)GameSettings.Instance.loc;
            
            _languageDropdown.onValueChanged.AddListener(DropdownValueChanged);
            
            _backButtonScript.AddOnClick(_menuScript.SettingsButtonClicked);

            _titleText.text = GetTitleTextForLoc(GameSettings.Instance.loc);
            
            GameSettings.OnLocChanged += OnGameSettingsLocChanged;
        }

        private void OnGameSettingsLocChanged(Loc loc)
        {
            _titleText.text = GetTitleTextForLoc(loc);
        }

        private string GetTitleTextForLoc(Loc loc)
        {
            return _titleTranslatedData.Find(e => e.loc == loc).content;
        }

        private void OnDestroy()
        {
            _masterSliderBox.ClearListener();
            _sfxSliderBox.ClearListener();
            _voicesSliderBox.ClearListener();
            GameSettings.OnLocChanged -= OnGameSettingsLocChanged;
        }

        private void DropdownValueChanged(int val)
        {
            switch (val)
            {
                case 0:
                    GameSettings.SetLoc(Loc.FR_fr);
                    Debug.Log("English");
                    break;
                case 1:
                    GameSettings.SetLoc(Loc.EN_en);
                    Debug.Log("French");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void MasterSliderValueChanged(float value)
        {
            float val = -80 + value * 80;
            PlayerPrefs.SetFloat("volume_master", value);
            Mixer.SetFloat("volume_master", val);
        }

        private void SfxSliderValueChanged(float value)
        {
            float var = -80 + value * 80;
            PlayerPrefs.SetFloat("volume_sfx", value);
            Mixer.SetFloat("volume_sfx", var);
        }
        
        private void VoicesSliderValueChanged(float value)
        {
            float var = -80 + value * 80;
            PlayerPrefs.SetFloat("volume_voices", value);
            Mixer.SetFloat("volume_voices", var);
        }
        
    }
}