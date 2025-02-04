using UnityEngine;

namespace _project.Scripts.KitchenScripts
{
    public class SinkWater : MonoBehaviour
    {
        private CleanableItemConstraint _cleanedItem;
        [SerializeField] private float _cleaningTime = 2f;
        private float _cleanStartTime;
        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
            CleanableItemConstraint cleanableItemConstraint = collision.GetComponent<CleanableItemConstraint>();
            if (cleanableItemConstraint != null)
            {
                _cleanedItem = cleanableItemConstraint;
                _cleanStartTime = Time.time;
            }
        }

        public void ChangeActivation()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            CleanableItemConstraint cleanableItemConstraint = collision.GetComponent<CleanableItemConstraint>();
            if (cleanableItemConstraint != null && _cleanedItem == cleanableItemConstraint)
            {
                _cleanedItem = null;
            }
        }

        public void Update()
        {
            if (!_cleanedItem) return;
            if (_cleanStartTime + _cleaningTime < Time.time)
            {
                _cleanedItem.Clean();
            }
        }
    }
}
