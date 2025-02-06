using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

namespace _project.Scripts
{
    public class TouchDetectionZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onTouchDetected;
        public void OnClick(TouchState touchState)
        {
            _onTouchDetected?.Invoke();
        }

        public void DebugLog(string message)
        {
            Debug.Log(message);
        }

        public void MoveTo(SceneElement sceneElement)
        {
            if (SceneManager.Instance != null)
            {
                SceneManager.Instance.MoveTo(sceneElement, transform.position);
            }
        }

        public void ChangeActivationState(GameObject gameObjectRef)
        {
            gameObjectRef.SetActive(!gameObjectRef.activeSelf);
        }
    }
}
