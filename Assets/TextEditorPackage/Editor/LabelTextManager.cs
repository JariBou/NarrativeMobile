using System;
using System.Collections.Generic;

public class StylizedCharList
{
    public List<StylizedChar> StylizedText;

    //returns a copy of the StylizedText
    public List<StylizedChar> GetStylizedTextCopy()
    {
        List<StylizedChar> stylizedTextCopy = new List<StylizedChar>();
        foreach (StylizedChar stylizedChar in StylizedText)
        {
            stylizedTextCopy.Add(new StylizedChar(stylizedChar.Char, stylizedChar.TextData.Clone()));
        }
        return stylizedTextCopy;
    }
    public StylizedCharList()
    {
        StylizedText = new List<StylizedChar>();
    }

    public string GetStringStylizedText()
    {
        string resultText = "";
        TextData activeTextData = null;
        foreach (StylizedChar stylizedChar in StylizedText)
        {
            resultText += stylizedChar.TextData.NecessaryBalise(activeTextData) + stylizedChar.Char;
            activeTextData = stylizedChar.TextData;
        }
        return resultText;
    }

    public string GetStringNotStylizedText()
    {
        string resultText = "";
        foreach (StylizedChar stylizedChar in StylizedText)
        {
            resultText += stylizedChar.Char;
        }
        return resultText;
    }

    public string GetStringStylizedText(int[] selectionIndexes)
    {
        int index = 0;
        string resultText = "";
        TextData activeTextData = null;
        foreach (StylizedChar stylizedChar in StylizedText)
        {
            if (index == selectionIndexes[0]) resultText += "<mark=#24cbd160>";
            resultText += stylizedChar.TextData.NecessaryBalise(activeTextData) + stylizedChar.Char;
            index++;
            if (index == selectionIndexes[1]) resultText += "</mark>";
            activeTextData = stylizedChar.TextData;
        }
        return resultText;
    }

    public void ApplyDataToSelectionIndexes(int[] selectionIndexes, TextData activeTextData)
    {
        for (int i = 0; i < StylizedText.Count; i++)
        {
            if (i >= selectionIndexes[0] && i < selectionIndexes[1])
            {
                StylizedText[i] = new StylizedChar(StylizedText[i].Char ,activeTextData.Clone());
            }
        }
    }

    #region MyRegion
    
    public void ApplySpecificDataToSelectionIndexes(int[] selectionIndexes, TextData activeTextData,
        TextDataType changedType)
    {
        for (int i = 0; i < StylizedText.Count; i++)
        {
            if (i >= selectionIndexes[0] && i < selectionIndexes[1])
            {
                TextData textData = StylizedText[i].TextData.Clone();
                switch (changedType)
                {
                    case TextDataType.Bold:
                        textData.IsBold = activeTextData.IsBold;
                        break;
                    case TextDataType.Italic:
                        textData.IsItalic = activeTextData.IsItalic;
                        break;
                    case TextDataType.Underline:
                        textData.IsUnderline = activeTextData.IsUnderline;
                        break;
                    case TextDataType.Size:
                        textData.Size = activeTextData.Size;
                        break;
                    case TextDataType.StrikeThrough:
                        textData.IsStrikeThrough = activeTextData.IsStrikeThrough;
                        break;
                    case TextDataType.Color:
                        textData.Color = activeTextData.Color;
                        break;
                    case TextDataType.Marker:
                        textData.Marker = new Marker(activeTextData.Marker.IsMarked, activeTextData.Marker.Color);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(changedType), changedType, null);
                }
                StylizedText[i] = new StylizedChar(StylizedText[i].Char ,textData.Clone());
            }
        }
    }
    #endregion
    
    //Check for Text Value Changed
    public void CheckStylizedText(string text, TextData currentTextData)
    {
        if (text.Length < StylizedText.Count)
        {
            CheckRemovedChars(text, currentTextData.Clone());
        }
        else if (text.Length > StylizedText.Count)
        {
            CheckAddedChars(text, currentTextData.Clone());
        }
        else
        {
            CheckModifiedChars(text, currentTextData.Clone());
        }
        
    }

    private void CheckModifiedChars(string text, TextData currentTextData)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != StylizedText[i].Char)
            {
                StylizedText[i] = new StylizedChar(text[i], currentTextData.Clone());
            }
        }
    }

    private void CheckAddedChars(string text, TextData currentTextData)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (StylizedText.Count <= i)
            {
                StylizedText.Add(new StylizedChar(text[i], currentTextData.Clone()));
            }
            else if (text[i] != StylizedText[i].Char)
            {
                StylizedText.Insert(i, new StylizedChar(text[i], currentTextData.Clone()));
            }
        }
    }

    private void CheckRemovedChars(string text, TextData currentTextData)
    {
        for (int i = 0; i < StylizedText.Count; i++)
        {
            while (text.Length <= i && StylizedText.Count > text.Length)
            {
                StylizedText.RemoveAt(i);
            }
            if (text.Length == StylizedText.Count) return;
            while (text[i] != StylizedText[i].Char)
            {
                StylizedText.RemoveAt(i);
                if (text.Length == StylizedText.Count) return;
            }
        }
        CheckModifiedChars(text, currentTextData);
    }
}
