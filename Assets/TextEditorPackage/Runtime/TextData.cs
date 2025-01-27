using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TextData
{
    public bool IsBold;
    public bool IsItalic;
    public bool IsUnderline;
    public bool IsStrikeThrough;
    public Marker Marker;
    public Color Color;
    public int Size;

    /*public Balise GetBalise(TextData previousTextData = null)
    {
        Balise balise = new Balise();
        if (_isBold && previousTextData is not { _isBold: true }) balise.AddBalise("<b>", "</b>");
        if (_isItalic && previousTextData is not { _isItalic: true }) balise.AddBalise("<i>", "</i>");
        if (_isUnderline && previousTextData is not { _isUnderline: true }) balise.AddBalise("<u>", "</u>");
        if (previousTextData == null || previousTextData._hexColorCode != _hexColorCode) balise.AddBalise("<color=#" + _hexColorCode +">", "</color>");
        if (previousTextData == null || previousTextData._size != _size) balise.AddBalise("<size=" + _size +">", "</size>");
        return balise;
    }*/

    public TextData(bool isBold, bool isItalic, bool isUnderline, bool isStrikeThrough,Marker marker, Color color, int size)
    {
        IsBold = isBold;
        IsItalic = isItalic;
        IsUnderline = isUnderline;
        IsStrikeThrough = isStrikeThrough;
        Marker = marker;
        Color = color;
        Size = size;
    }

    public TextData Clone()
    {
        return new TextData(IsBold, IsItalic, IsUnderline, IsStrikeThrough, new Marker(Marker.IsMarked, Marker.Color), Color, Size);
    }

    public string NecessaryEndBalise()
    {
        string resultBalise = "";
        
        if (IsItalic) resultBalise += "</i>";
        if (IsBold) resultBalise += "</b>";
        if (IsUnderline) resultBalise += "</u>";
        if (IsStrikeThrough) resultBalise += "</s>";
        if (Marker.IsMarked) resultBalise += "</mark>";
        resultBalise += "</color>";// + "</size>";
        return resultBalise;
    }
    public string NecessaryBalise(TextData activeTextData)
    {
        string resultBalise = "";

        if (activeTextData == null)
        {
            //resultBalise+= "<size=" + Size + ">";
            resultBalise+= "<color=#" + Color.ToHexString() +">";
            resultBalise += Marker.IsMarked? "<mark=#" + Marker.Color.ToHexString() + ">" : "";
            resultBalise+= IsBold ? "<b>" : "";
            resultBalise+= IsItalic ? "<i>" : "";
            resultBalise+= IsUnderline ? "<u>" : "";
            resultBalise+= IsStrikeThrough ? "<s>" : "";
            return resultBalise;
        }
        
        //Size
        if (Size != activeTextData.Size)
        {
            //resultBalise += "</size><size=" + Size + ">";
        }
        
        //Color
        if (Color != activeTextData.Color)
        {
            bool hasAppliedColorBalise = false;
            string colorBalise = "</color><color=#" + Color.ToHexString() + ">";
            //to fix color of underline and strikethrought depending on color of font

            if (IsUnderline && activeTextData.IsUnderline)
            {
                resultBalise += "</u>";
                resultBalise += colorBalise;
                resultBalise += "<u>";
                hasAppliedColorBalise = true;
            }
            if (IsStrikeThrough && activeTextData.IsStrikeThrough)
            {
                resultBalise += "</s>";
                resultBalise += colorBalise;
                resultBalise += "<s>";
                hasAppliedColorBalise = true;
            }
            if (!hasAppliedColorBalise) resultBalise += colorBalise;
        }

        if (Marker.IsMarked != activeTextData.Marker.IsMarked)
        {
            resultBalise += Marker.IsMarked? "<mark=#" + Marker.Color.ToHexString() + ">" : "</mark>";
        }
        else if (Marker.IsMarked && Marker.Color != activeTextData.Marker.Color)
        {
            resultBalise += "</mark><mark=#" + Marker.Color.ToHexString() + ">";
        }
        
        //Bold
        if (IsBold != activeTextData.IsBold)
        {
            resultBalise += IsBold ? "<b>" : "</b>";
        }
        
        //Italic
        if (IsItalic != activeTextData.IsItalic)
        {
            resultBalise += IsItalic ? "<i>" : "</i>";
        }
        
        //Underline
        if (IsUnderline != activeTextData.IsUnderline)
        {
            resultBalise += IsUnderline ? "<u>" : "</u>";
        }
        
        //StrikeThrought
        if (IsStrikeThrough != activeTextData.IsStrikeThrough)
        {
            resultBalise += IsStrikeThrough ? "<s>" : "</s>";
        }
        
        
        return resultBalise;
    }
}

[Serializable]
public struct Marker
{
    public bool IsMarked;
    public Color Color;

    public Marker(bool isMarked, Color color)
    {
        IsMarked = isMarked;
        Color = color;
    }
}

[Serializable]
public enum TextDataType
{
    Bold,
    Italic,
    Underline,
    Size,
    StrikeThrough,
    Color,
    Marker,
}
