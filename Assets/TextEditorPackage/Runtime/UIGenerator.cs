using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGenerator : MonoBehaviour
{
    public string _savedTextFileName;
    
    public Sprite _backgroundSprite = null;
    public bool _autoSizeBackgroundSprite = false;
    public Vector3 _backgroundSize;
    
    private SavedTextSO _savedTextSo;
    
    
    public void GenerateUIElement()
    {
        if (_savedTextFileName != "")
        {
            _savedTextSo = SavedTextManager.LoadText(_savedTextFileName);
        }
        
        if (_savedTextSo == null) return;
        Canvas activeCanvas = GetComponent<Canvas>();
        if (activeCanvas != null)
        {
            Image image = new GameObject("UITextBackground " + _savedTextFileName).AddComponent<Image>();
            image.transform.SetParent(activeCanvas.transform);
            image.transform.position = activeCanvas.transform.position;
            if (_backgroundSprite != null) image.sprite = _backgroundSprite;
            TextMeshPro text = new GameObject("UIText " + _savedTextFileName).AddComponent<TextMeshPro>();
            text.transform.SetParent(image.transform);
            text.transform.position = image.transform.position;
            text.transform.position += new Vector3(0f, 0f, -1f);
            text.richText = true;
            text.text = _savedTextSo.GetTextRichFormat();
            text.autoSizeTextContainer = true;
            
            if (!_autoSizeBackgroundSprite) image.rectTransform.sizeDelta = _backgroundSize;
            else
            {
                image.transform.localScale = text.rectTransform.sizeDelta;
            }
        }
    }
}