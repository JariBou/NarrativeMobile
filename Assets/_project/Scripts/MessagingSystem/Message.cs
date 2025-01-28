using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
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