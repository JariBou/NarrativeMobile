using System;
using System.Collections;
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
        private ContactButtonScript _linkedButton;
        private Coroutine _startCoroutine;

        public PhoneUser userTarget => _userTarget;
        public ContactButtonScript linkedButton => _linkedButton;

        public void AddMessage(Message message, FDateTime overrideTime = null)
        {
            TextAnchor textAnchor = message._user.UserId == "player_user" ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            messageScript.SetText(message._content, textAnchor);
            _messages.Add(messageScript);
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
            RefreshSizes();
            linkedButton.SetNewDate(overrideTime ?? message._sentDateTime);
        }
        
        public void AddMessageTest(string message, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            messageScript.SetText(message, textAnchor);
            _messages.Add(messageScript);
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
            RefreshSizes();
            linkedButton.SetNewDate(new FDateTime(5, 10));
        }

        public void SetUser(PhoneUser user)
        {
            _userTarget = user;
        }

        public void LinkButton(ContactButtonScript button)
        {
            _linkedButton = button;
        }

        private void OnEnable()
        {
            // _linkedButton.image.color = Color.green;
        }

        private void OnDisable()
        {
            // _linkedButton.image.color = Color.red;
        }

        public void RefreshSizes()
        {
            // foreach (MessageScript messageScript in _messages)
            // {
            //     messageScript.RefreshSize();
            // }
            // foreach (MessageScript messageScript in _messages)
            // {
            //     messageScript.gameObject.SetActive(false);
            //     messageScript.RefreshSize();
            //     messageScript.gameObject.SetActive(true);
            // }
            if (!gameObject.activeSelf) return;
            if (_startCoroutine != null) StopCoroutine(_startCoroutine);
            _startCoroutine = StartCoroutine(RefreshSizesRoutine());
        }

        private IEnumerator RefreshSizesRoutine()
        {
            yield return new WaitForEndOfFrame();
            foreach (MessageScript messageScript in _messages)
            {
                messageScript.gameObject.SetActive(false);
                messageScript.RefreshSize();
                messageScript.gameObject.SetActive(true);
            }
            _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
            // LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.GetComponentInParent<RectTransform>());
            // Canvas.ForceUpdateCanvases();
        }
    }
}