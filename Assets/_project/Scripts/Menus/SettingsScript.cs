using System;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class SettingsScript : MonoBehaviour
    {
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _voicesSlider;
        [SerializeField] private TMP_Dropdown _languageDropdown;
        [SerializeField] private Button _backButton;
        [SerializeField] private MainMenuScript _menuScript;
        
        [SerializeField]
        private AudioMixer Mixer;

        private void Awake()
        {
            _masterSlider.value = PlayerPrefs.GetFloat("volume_master");
            float val = -80 + _masterSlider.value * 80;
            Mixer.SetFloat("volume_master", val);
            
            _sfxSlider.value = PlayerPrefs.GetFloat("volume_sfx");
            val = -80 + _sfxSlider.value * 80;
            Mixer.SetFloat("volume_sfx", val);
            
            _voicesSlider.value = PlayerPrefs.GetFloat("volume_voices");
            val = -80 + _voicesSlider.value * 80;
            Mixer.SetFloat("volume_voices", val);
            
            _masterSlider.onValueChanged.AddListener(MasterSliderValueChanged);
            _sfxSlider.onValueChanged.AddListener(SfxSliderValueChanged);
            _voicesSlider.onValueChanged.AddListener(VoicesSliderValueChanged);
            
            _languageDropdown.options.Clear();
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData("English"));
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData("French"));
            _languageDropdown.value = 0;
            
            _languageDropdown.onValueChanged.AddListener(DropdownValueChanged);
            
            _backButton.onClick.AddListener(_menuScript.SettingsButtonClicked);
        }

        private void DropdownValueChanged(int val)
        {
            switch (val)
            {
                case 0:
                    GameSettings.SetLoc(Loc.EN_en);
                    Debug.Log("English");
                    break;
                case 1:
                    GameSettings.SetLoc(Loc.FR_fr);
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