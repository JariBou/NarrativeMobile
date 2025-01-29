using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class Phone : MonoBehaviour
    {
        public List<PhoneUser> _phoneUsers = new();
        public Dictionary<string, PhoneConversation> _conversationDictionnary = new();
        public Transform _conversationParentTransform;
        public Transform _contactButtonTransform;
        public GameObject _conversationPrefab;
        public GameObject _buttonPrefab;

        private bool _hasNotification;

        private void Awake()
        {
            foreach (PhoneUser user in _phoneUsers)
            {
                PhoneConversation phoneConversation = Instantiate(_conversationPrefab, _conversationParentTransform).GetComponent<PhoneConversation>();
                phoneConversation.SetUser(user);
                _conversationDictionnary.Add(user.UserId, phoneConversation);
                Button button = Instantiate(_buttonPrefab, _contactButtonTransform).GetComponent<Button>();
                button.onClick.AddListener(delegate { OnContactClicked(user.UserId); });
                button.GetComponentInChildren<TMP_Text>().text = user.UserId;
                phoneConversation.LinkButton(button);
                phoneConversation.gameObject.SetActive(false);
            }  
            
            OnContactClicked(_conversationDictionnary.Keys.ElementAt(0));
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");

        }

        private void Start()
        {
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");
            _conversationDictionnary["test_user"].AddMessageTest("AAAAAAA this is some test shit");
            _conversationDictionnary["test_user"].AddMessageTest("AAAAAAA this is some test shit", TextAnchor.MiddleRight);
            _conversationDictionnary["test_user"].AddMessageTest("AAAAAAA this is some test shit");
            // MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            // messageScript.SetText("AAAAAAA this is some test shit (but sent by me)", TextAnchor.MiddleRight);
            // _scrollRect.normalizedPosition = new Vector2(0, 0);
        }

        public void AddMessage(Message message, string conversationUserId)
        {
            // _conversationDictionnary[conversationUserId].Add(message);
            // _hasNotification = true;
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText(message._content);
            // _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }

        public void OnPhonePullUp()
        {
            _hasNotification = false;
        }

        public void OnContactClicked(string userId)
        {
            foreach (string key in _conversationDictionnary.Keys)
            {
                _conversationDictionnary[key].gameObject.SetActive(key == userId);
            }
            // List<Message> messages = _conversationDictionnary[_phoneUsers[contactIndex].UserId];
            
        }
    }
}