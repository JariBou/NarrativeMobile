using System;
using UnityEngine;

[Serializable]
public struct StylizedChar
{
    public char Char;
    [SerializeField] public TextData TextData;

    public StylizedChar(char c, TextData currentTextData)
    {
        Char = c;
        TextData = currentTextData;
    }
}
