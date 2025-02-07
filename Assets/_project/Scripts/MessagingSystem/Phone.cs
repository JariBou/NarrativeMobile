using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private List<PhoneUser> _phoneUsers = new();
        private Dictionary<string, PhoneConversation> _conversationDictionnary = new();
        [SerializeField] private Transform _conversationParentTransform;
        [SerializeField] private Transform _contactButtonTransform;
        [SerializeField] private GameObject _conversationPrefab;
        [SerializeField] private GameObject _buttonPrefab;

        [SerializeField] private Image _phoneBgImage;
        [SerializeField] private Image _messageDetailIcon;
        [SerializeField] private TMP_Text _messageDetailName;
        
        [SerializeField] private NotificationScript _notificationScript;
        
        [SerializeField] private List<GameObject> _phoneAppPanels = new();
        [SerializeField] private List<Sprite> _bgSprites;
        
        [SerializeField] private List<Message> _initialMessages = new();

        private bool _hasNotification;
        private string _selectedConv;

        private void Awake()
        {
            // foreach (PhoneUser user in _phoneUsers)
            // {
            //     PhoneConversation phoneConversation = Instantiate(_conversationPrefab, _conversationParentTransform).GetComponent<PhoneConversation>();
            //     phoneConversation.SetUser(user);
            //     _conversationDictionnary.Add(user.UserId, phoneConversation);
            //     ContactButtonScript button = Instantiate(_buttonPrefab, _contactButtonTransform).GetComponent<ContactButtonScript>();
            //     // button.button.onClick.AddListener(delegate { OnContactClicked(user.UserId); });
            //     button.Init(user, delegate { OnContactClicked(user.UserId); });
            //     // button.GetComponentInChildren<TMP_Text>().text = user.UserId;
            //     phoneConversation.LinkButton(button);
            //     phoneConversation.gameObject.SetActive(false);
            // }  
            
            // OnContactClicked(_conversationDictionnary.Keys.ElementAt(0));
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");

        }

        public void OnPhoneAppClicked(int index)
        {
            for (int i = 0; i < _phoneAppPanels.Count; i++)
            {
                _phoneAppPanels[i].SetActive(i == index);
            }
            _phoneBgImage.sprite = _bgSprites[index];
        }

        private void Start()
        {
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText("AAAAAAA this is some test shit");
            // AddMessageTest("Your need to go to this place !", "test_user");
            // AddMessageTest("What is the address ?", "test_user", TextAnchor.MiddleRight);
            // AddMessageTest("It's the 10th on Nanana street", "test_user");
            // MessageScript messageScript = Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>();
            // messageScript.SetText("AAAAAAA this is some test shit (but sent by me)", TextAnchor.MiddleRight);
            // _scrollRect.normalizedPosition = new Vector2(0, 0);

            foreach (Message message in _initialMessages)
            {
                AddMessage(message, message.User.UserId, false);
            }
            
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.position;
            
            _pulledDownPosition = _startPosition - new Vector3(0, Camera.main.pixelHeight + 100, 0);
            
            _rectTransform.position = _pulledDownPosition;
            
            if (_conversationDictionnary.Count > 0)
            {
                OnContactClicked(_conversationDictionnary.Keys.ElementAt(0));
            }
        }

        public void AddMessage(Message message, string conversationUserId, bool playNotifSound = true)
        {
            if (!_conversationDictionnary.ContainsKey(conversationUserId))
            {
                if (!InitializeConversation(conversationUserId)) return;
            }
            
            _conversationDictionnary[conversationUserId].AddMessage(message);
            _conversationDictionnary[conversationUserId].linkedButton.GetComponent<RectTransform>().SetAsFirstSibling();
            _notificationScript.NotifyNewMessage(playNotifSound);

            // _conversationDictionnary[conversationUserId].Add(message);
            // _hasNotification = true;
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText(message._content);
            // _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }
        
        public void AddMessageTest(string message, string conversationUserId, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            if (!_conversationDictionnary.ContainsKey(conversationUserId))
            {
                if (!InitializeConversation(conversationUserId)) return;
            }
            _conversationDictionnary[conversationUserId].AddMessageTest(message, textAnchor);
            // _conversationDictionnary[conversationUserId].Add(message);
            // _hasNotification = true;
            // Instantiate(_messagePrefab, _contentPanel).GetComponent<MessageScript>().SetText(message._content);
            // _scrollRect.normalizedPosition = new Vector2(0, 0); // maybe put when you pull up your phone or smth like that
        }

        private bool InitializeConversation(string conversationUserId)
        {
            PhoneUser user = _phoneUsers.FirstOrDefault(u => u.UserId == conversationUserId);
            if (user == null) return false;
                
            PhoneConversation phoneConversation = Instantiate(_conversationPrefab, _conversationParentTransform).GetComponent<PhoneConversation>();
            phoneConversation.SetUser(user);
            _conversationDictionnary.Add(user.UserId, phoneConversation);
            ContactButtonScript button = Instantiate(_buttonPrefab, _contactButtonTransform).GetComponent<ContactButtonScript>();
            // button.button.onClick.AddListener(delegate { OnContactClicked(user.UserId); });
            button.Init(user, delegate { OnContactClicked(user.UserId); });
            // button.GetComponentInChildren<TMP_Text>().text = user.UserId;
            phoneConversation.LinkButton(button);
            phoneConversation.gameObject.SetActive(false);
            return true;
        }

        private bool _isPullUp;
        private float _timer = 1;
        [SerializeField] private float _phonePullUpAnimTime = 2f;
        [SerializeField] private AnimationCurve _phonePullUpAnimTimeCurve;
        [SerializeField] private float _phonePullDownAnimTime = 1.5f;
        [SerializeField] private AnimationCurve _phonePullDownAnimTimeCurve;
        private Vector3 _startPosition;
        private Vector3 _pulledDownPosition;
        private RectTransform _rectTransform;

        private void Update()
        {
            if (_timer >= 1)
            {
                if (!_isPullUp)
                {
                    gameObject.SetActive(false);
                }
                return;
            }
            _timer += Time.deltaTime / ( _isPullUp ? _phonePullUpAnimTime : _phonePullDownAnimTime );

            if (_isPullUp)
            {
                _rectTransform.position = Vector3.Lerp(_pulledDownPosition, _startPosition, _phonePullUpAnimTimeCurve.Evaluate(_timer));
            }
            else
            {
                _rectTransform.position = Vector3.Lerp(_startPosition, _pulledDownPosition, _phonePullDownAnimTimeCurve.Evaluate(_timer));
            }


        }

        public void OnPhonePullUp()
        {
            _timer = 0;
            _isPullUp = true;
            _hasNotification = false;
            if (_selectedConv != null && _conversationDictionnary.ContainsKey(_selectedConv)) _conversationDictionnary[_selectedConv].RefreshSizes();
            else if (_selectedConv == null && _conversationDictionnary.Count > 0)
            {
                OnContactClicked(_conversationDictionnary.Keys.ElementAt(0));
            }
        }

        public void PullPhoneDown()
        {
            if (!_isPullUp && _timer < 1) return;
            _timer = 0;
            _isPullUp = false;
        }

        public void OnContactClicked(string userId)
        {
            foreach (string key in _conversationDictionnary.Keys)
            {
                _conversationDictionnary[key].gameObject.SetActive(key == userId);
                if (key == userId)
                {
                    _selectedConv = key;
                    _conversationDictionnary[key].RefreshSizes();
                    _messageDetailIcon.sprite = _conversationDictionnary[key].userTarget.Icon;
                    _messageDetailName.text = _conversationDictionnary[key].userTarget.GetNameFor(GameSettings.Instance.loc);
                }
            }
            // List<Message> messages = _conversationDictionnary[_phoneUsers[contactIndex].UserId];
            
        }
    }
}