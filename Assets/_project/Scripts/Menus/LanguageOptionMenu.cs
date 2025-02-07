using System;
using _project.Scripts;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;

public class LanguageOptionMenu : MonoBehaviour
{
    [SerializeField] private TranslatedData[] _languageOptions;
    [SerializeField] private TextMeshProUGUI _languageOptionText;
    private int _currentLanguageIndex = 0;
    
    public event Action<Loc> OnLanguageChanged;

    private void Start()
    {
        Loc loc = GameSettings.Instance.loc;
        _currentLanguageIndex = 0;
        for (int i = 0; i < _languageOptions.Length; i++)
        {
            if (_languageOptions[i].loc == loc)
            {
                _languageOptionText.text = _languageOptions[i].content;
                _currentLanguageIndex = i;
            }
        }
    }

    public void ChangeLanguage(bool increaseIndex)
    {
        int nextIndex = increaseIndex ? _currentLanguageIndex + 1 : _currentLanguageIndex - 1 ;
        if (nextIndex < 0) nextIndex = _languageOptions.Length - 1;
        else if (nextIndex >= _languageOptions.Length) nextIndex = 0;
        _currentLanguageIndex = nextIndex;
        _languageOptionText.text = _languageOptions[_currentLanguageIndex].content;
        OnLanguageChanged?.Invoke(_languageOptions[_currentLanguageIndex].loc);
    }
}
