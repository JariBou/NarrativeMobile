using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    public class NotificationScript : MonoBehaviour
    {
        
        [SerializeField] private GameObject _notificationIcon;

        [SerializeField] private GameObject _phoneGameobject;
        
        public void NotifyNewMessage()
        {
            if (!_phoneGameobject.activeSelf)
            {
                _notificationIcon.SetActive(true);
            }
        }

        public void PhonePulledUp()
        {
            _notificationIcon.SetActive(false);
        }
        
    }
}