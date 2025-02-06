using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    public class MessagingService : MonoBehaviour
    {
        public static MessagingService Instance { get; private set; }
        
        private int _lastSentMessageTimeIndex = 0;
        public List<FDateTime> _messageSendingTime = new();

        public Phone _phone;
        
        private void Awake()
        {
            Instance = this;
        }

        public void SendMessage(Message message, string conversationId, bool playNotifSound = true)
        {
            // new Message(_messageSendingTime[_lastSentMessageTimeIndex], "textContent", _userTest);
            // _lastSentMessageTimeIndex++;
            _phone.AddMessage(message, conversationId, playNotifSound);
        }
        
    }



    
}