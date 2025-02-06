using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.MessagingSystem
{
    public class NotificationScript : MonoBehaviour
    {
        [SerializeField] private bool _doAnimCurve;
        [SerializeField] private Image _notificationIconImg;
        [SerializeField] private AnimationCurve _notifColorLerpAnimCurve;
        [SerializeField] private float _animSpeed = .7f;
        [SerializeField] private Animator _notifIconAnimator;

        [SerializeField] private GameObject _phoneGameobject;
        [SerializeField] private AudioClip _notifSound;
        
        public void NotifyNewMessage(bool playNotifSound)
        {
            if (!_phoneGameobject.activeSelf)
            {
                if (!_notificationIconImg.gameObject.activeSelf)
                {
                    _notificationIconImg.gameObject.SetActive(true);
                    _notifIconAnimator.SetTrigger("PlayAnim");
                }
                if (playNotifSound && _notifSound is not null)
                {
                    AudioManager.PlaySfx(_notifSound);
                }
            }
        }

        public void PhonePulledUp()
        {
            _notificationIconImg.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_doAnimCurve && _notificationIconImg.gameObject.activeSelf)
            {
                _notificationIconImg.color = Color.Lerp(Color.white, Color.red, _notifColorLerpAnimCurve.Evaluate(Mathf.PingPong(_animSpeed * Time.time, 1f)));
            }
        }
    }
}