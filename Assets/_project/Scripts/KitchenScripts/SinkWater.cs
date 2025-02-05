using UnityEngine;

namespace _project.Scripts.KitchenScripts
{
    public class SinkWater : MonoBehaviour
    {
        private CleanableItemConstraint _cleanedItem;
        [SerializeField] private float _cleaningTime = 2f;
        [SerializeField] private GameObject _splashObject;
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
            _splashObject.SetActive(_cleanedItem);
            if (!_cleanedItem) return;
            _splashObject.transform.position = new Vector3(_splashObject.transform.position.x, _cleanedItem.transform.position.y, _splashObject.transform.position.z);
            if (_cleanStartTime + _cleaningTime < Time.time)
            {
                _cleanedItem.Clean();
            }
        }
    }
}
