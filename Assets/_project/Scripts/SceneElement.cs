using UnityEngine;
using UnityEngine.Events;

namespace _project.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SceneElement : MonoBehaviour
    {
        [SerializeField] private bool _isMainRoom;
        [SerializeField] private UnityEvent _onFirstEnterRoom;
        private bool _hasEnteredRoom;
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Sprite GetSceneSprite()
        {
            return GetComponent<SpriteRenderer>().sprite;
        }

        public bool IsMainRoom() => _isMainRoom;

        public void EnterRoom()
        {
            if (_hasEnteredRoom) return;
            _hasEnteredRoom = true;
            _onFirstEnterRoom?.Invoke();
        }
 
    }
}
