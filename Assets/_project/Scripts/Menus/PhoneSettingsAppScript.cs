using System;
using System.Collections.Generic;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace _project.Scripts.Menus
{
    public class PhoneSettingsAppScript : MonoBehaviour
    {
        // [SerializeField] private Slider _masterSlider;
        [SerializeField] private SliderBoxScript _masterSliderScript;
        // [SerializeField] private Slider _sfxSlider;
        [SerializeField] private SliderBoxScript _musicSliderBox;
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
            float val = _masterSliderScript.GetMixerValue();
            Mixer.SetFloat("volume_master", val);
            
            _musicSliderBox.Init(PlayerPrefs.GetFloat("volume_music"));
            val = _musicSliderBox.GetMixerValue();
            Mixer.SetFloat("volume_music", val);
            
            _sfxSliderScript.Init(PlayerPrefs.GetFloat("volume_sfx"));
            val = _sfxSliderScript.GetMixerValue();
            Mixer.SetFloat("volume_sfx", val);
            
            _voicesSliderScript.Init(PlayerPrefs.GetFloat("volume_voices"));
            val = _voicesSliderScript.GetMixerValue();
            Mixer.SetFloat("volume_voices", val);
            
            _masterSliderScript.AddListener(MasterSliderValueChanged);
            _musicSliderBox.AddListener(MusicSliderValueChanged);
            _sfxSliderScript.AddListener(SfxSliderValueChanged);
            _voicesSliderScript.AddListener(VoicesSliderValueChanged);
            
            _settingsText.text = GetTextFor(GameSettings.Instance.loc, _settingsTranslations);
            _userText.text = GetTextFor(GameSettings.Instance.loc, _userTranslations);
            GameSettings.OnLocChanged += GameSettingsLocChanged;
        }

        private void OnDestroy()
        {
            _masterSliderScript.ClearListener();
            _musicSliderBox.ClearListener();
            _sfxSliderScript.ClearListener();
            _voicesSliderScript.ClearListener();
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
        
        private void MusicSliderValueChanged(float value)
        {
            float var = -80 + value * 80;
            PlayerPrefs.SetFloat("volume_music", value);
            Mixer.SetFloat("volume_music", var);
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