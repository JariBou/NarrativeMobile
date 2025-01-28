using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

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
}
