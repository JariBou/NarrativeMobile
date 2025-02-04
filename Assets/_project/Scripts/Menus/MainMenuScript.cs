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
        
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _mainMenuPanel;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayButtonClicked);
            _settingsButton.onClick.AddListener(SettingsButtonClicked);
            _quitButton.onClick.AddListener(QuitButtonClicked);
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
            throw new NotImplementedException();
        }
    }
}