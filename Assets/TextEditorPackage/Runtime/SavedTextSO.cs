using System.Collections.Generic;
using UnityEngine;

public class SavedTextSO : ScriptableObject
{
    [SerializeField] private string _fileName;
    [SerializeField] private List<StylizedChar> _stylizedText;

    public void SetupValues(string fileName, List<StylizedChar> stylizedText)
    {
        _fileName = fileName;
        _stylizedText = stylizedText;
    }
    public string GetFileName() => _fileName;

    public List<StylizedChar> GetStylizedTextCopy()
    {
        List<StylizedChar> stylizedText = new List<StylizedChar>();
        foreach (StylizedChar stylizedChar in _stylizedText)
        {
            stylizedText.Add(new StylizedChar(stylizedChar.Char, stylizedChar.TextData.Clone()));
        }
        return stylizedText;
    }

    public string GetTextRichFormat()
    {
        string resultText = "";
        TextData activeTextData = null;
        foreach (StylizedChar stylizedChar in _stylizedText)
        {
            resultText += stylizedChar.TextData.NecessaryBalise(activeTextData) + stylizedChar.Char;
            activeTextData = stylizedChar.TextData;
        }

        if (activeTextData != null) resultText += activeTextData.NecessaryEndBalise();
        return resultText;
    }

    public string GetTextContent()
    {
        string resultText = "";
        foreach (StylizedChar stylizedChar in _stylizedText)
        {
            resultText += stylizedChar.Char;
        }
        return resultText;
    }
}
