using System.Collections.Generic;
using _project.Scripts;
using _project.Scripts.Localisation;
using TMPro;
using UnityEngine;

public class TextTranslator : MonoBehaviour
{
    [SerializeField] private List<TranslatedData> _textContent;
    [SerializeField] private TextMeshProUGUI _textOverride;

    private void Start()
    {
        if (_textOverride == null) _textOverride = GetComponentInChildren<TextMeshProUGUI>();
        if (_textOverride == null) Debug.LogWarning("No TextMeshPro found on " + gameObject.name);
        _textOverride.text = GetTextFromLoc(GameSettings.Instance.loc);
    }

    private string GetTextFromLoc(Loc loc)
    {
        foreach (TranslatedData translatedData in _textContent)
        {
            if (translatedData.loc == loc) return translatedData.content;
        }
        return "No translation found for loc: " + loc;
    }
}
