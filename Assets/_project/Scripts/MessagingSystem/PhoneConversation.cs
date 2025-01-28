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
        List<Message> _messages = new();
        private PhoneUser _userTarget;
        private Button _linkedButton;

        public PhoneUser userTarget => _userTarget;

        public void AddMessage(Message message)
        {
            _messages.Add(message);
            Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText(message._content);
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }
        
        public void AddMessageTest(string message, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText(message, textAnchor);
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
    }
}