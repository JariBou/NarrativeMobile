using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class PhoneConversation : MonoBehaviour
    {
        [SerializeField] private Transform _contentPanel;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _messagePrefab;
        List<MessageScript> _messages = new();
        private PhoneUser _userTarget;
        private Button _linkedButton;

        public PhoneUser userTarget => _userTarget;

        public void AddMessage(Message message)
        {
            TextAnchor textAnchor = message._user.UserId == "player_user" ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            messageScript.SetText(message._content, textAnchor);
            _messages.Add(messageScript);
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }
        
        public void AddMessageTest(string message, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            messageScript.SetText(message, textAnchor);
            _messages.Add(messageScript);
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }

        public void SetUser(PhoneUser user)
        {
            _userTarget = user;
        }

        public void LinkButton(Button button)
        {
            _linkedButton = button;
        }

        private void OnEnable()
        {
            _linkedButton.image.color = Color.green;
        }

        private void OnDisable()
        {
            _linkedButton.image.color = Color.red;
        }

        public void RefreshSizes()
        {
            foreach (MessageScript messageScript in _messages)
            {
                messageScript.RefreshSize();
            }
        }
    }
}