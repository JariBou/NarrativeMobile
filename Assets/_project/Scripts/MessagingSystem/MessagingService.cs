using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    public class MessagingService
    {
        private int _lastSentMessageTimeIndex = 0;
        public List<FDateTime> _messageSendingTime = new();

        public Phone _phone;

        public PhoneUser _userTest;

        public void SendMessage(Message message)
        {
            new Message(_messageSendingTime[_lastSentMessageTimeIndex], "textContent", _userTest);
            _lastSentMessageTimeIndex++;
        }
        
    }



    [CreateAssetMenu(fileName = "Message0", menuName = "MessagingSystem/Message")]
    public class Message : ScriptableObject
    {
        public FDateTime _sentDateTime;
        public string _content;
        public PhoneUser _user;

        public Message(FDateTime sentDateTime, string content, PhoneUser user)
        {
            _sentDateTime = sentDateTime;
            _content = content;
            _user = user;
        }
    }
}