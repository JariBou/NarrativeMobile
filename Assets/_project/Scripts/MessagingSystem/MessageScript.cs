using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class MessageScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RectTransform _bgRectTransform;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

        public void SetText(string text, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            _text.text = text;
            _verticalLayoutGroup.childAlignment = textAnchor;
            _verticalLayoutGroup.padding.left = textAnchor == TextAnchor.MiddleRight ? 100 : 10;
            _verticalLayoutGroup.padding.right = textAnchor == TextAnchor.MiddleLeft ? 100 : 10;
            
            StartCoroutine(UpdateSize());
        }

        public IEnumerator UpdateSize()
        {
            yield return new WaitForEndOfFrame();
            RectTransform rectTransform = GetComponent<RectTransform>();
            Rect rect = rectTransform.rect;
            rect.height =  _bgRectTransform.rect.height;
            Debug.Log(rect.height);
            Debug.Log(rectTransform.rect.height);
            // rectTransform.rect.Set(rect.x, rect.y, rect.width, rect.height);
            rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
            Debug.Log(rectTransform.rect.height);
        }
    }
}