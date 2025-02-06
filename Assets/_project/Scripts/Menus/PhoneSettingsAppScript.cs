using System.Collections.Generic;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class PhoneSettingsAppScript : MonoBehaviour
    {
        // [SerializeField] private Slider _masterSlider;
        [SerializeField] private SliderBoxScript _masterSliderScript;
        // [SerializeField] private Slider _sfxSlider;
        [SerializeField] private SliderBoxScript _sfxSliderScript;
        // [SerializeField] private Slider _voicesSlider;
        [SerializeField] private SliderBoxScript _voicesSliderScript;
        
        [SerializeField] private TMP_Text _settingsText;
        [SerializeField] private List<TranslatedData> _settingsTranslations;
        
        [SerializeField] private TMP_Text _userText;
        [SerializeField] private List<TranslatedData> _userTranslations;
        
        [SerializeField]
        private AudioMixer Mixer;

        private void Awake()
        {
            _masterSliderScript.Init(PlayerPrefs.GetFloat("volume_master"));
            float val = -80 + _masterSliderScript.GetValue() * 80;
            Mixer.SetFloat("volume_master", val);
            
            _sfxSliderScript.Init(PlayerPrefs.GetFloat("volume_sfx"));
            val = -80 + _sfxSliderScript.GetValue() * 80;
            Mixer.SetFloat("volume_sfx", val);
            
            _voicesSliderScript.Init(PlayerPrefs.GetFloat("volume_voices"));
            val = -80 + _voicesSliderScript.GetValue() * 80;
            Mixer.SetFloat("volume_voices", val);
            
            _masterSliderScript.AddListener(MasterSliderValueChanged);
            _sfxSliderScript.AddListener(SfxSliderValueChanged);
            _voicesSliderScript.AddListener(VoicesSliderValueChanged);
            
            _settingsText.text = GetTextFor(GameSettings.Instance.loc, _settingsTranslations);
            _userText.text = GetTextFor(GameSettings.Instance.loc, _userTranslations);
            GameSettings.OnLocChanged += GameSettingsLocChanged;
        }

        private void GameSettingsLocChanged(Loc loc)
        {
            _settingsText.text = GetTextFor(loc, _settingsTranslations);
            _userText.text = GetTextFor(loc, _userTranslations);
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
        
        public void Quit() { Application.Quit(); }

        public void MainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        
        public string GetTextFor(Loc loc, List<TranslatedData> translations)
        {
            return translations.Find(e => e.loc == loc).content;
        }
    }
}