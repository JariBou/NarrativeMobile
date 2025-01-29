using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class ContactButtonScript : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [FormerlySerializedAs("_text")] [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _lastMessageDateText;
        [SerializeField] private Image _icon;

        public Button button => _button;
        public TMP_Text nameText => _nameText;
        public Image icon => _icon;

        public void Init(PhoneUser user, UnityAction buttonCallback)
        {
            nameText.text = user.Name;
            _icon.sprite = user.Icon;
            button.onClick.AddListener(buttonCallback);
        }

        public void SetNewDate(FDateTime messageSentDateTime)
        {
            _lastMessageDateText.text = messageSentDateTime.GetDayMonthString();
        }
    }
}