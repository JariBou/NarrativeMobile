using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Menus
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TranslatedButtonScript _playButtonScript;
        [SerializeField] private TranslatedButtonScript _settingsButtonScript;
        [SerializeField] private TranslatedButtonScript _quitButtonScript;
        [SerializeField] private TranslatedButtonScript _creditsButtonScript;
        
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _mainMenuPanel;

        private void Awake()
        {
            // _playButton.onClick.AddListener(PlayButtonClicked);
            _playButtonScript.AddOnClick(PlayButtonClicked);
            // _settingsButton.onClick.AddListener(SettingsButtonClicked);
            _settingsButtonScript.AddOnClick(SettingsButtonClicked);
            // _quitButton.onClick.AddListener(QuitButtonClicked);
            _quitButtonScript.AddOnClick(QuitButtonClicked);
            _creditsButtonScript.AddOnClick(CreditsButtonClicked);
        }

        private void CreditsButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }

        private void QuitButtonClicked()
        {
            Application.Quit();
        }

        public void SettingsButtonClicked()
        {
            _settingsPanel.SetActive(!_settingsPanel.activeSelf);
            _mainMenuPanel.SetActive(!_mainMenuPanel.activeSelf);
        }

        private void PlayButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}