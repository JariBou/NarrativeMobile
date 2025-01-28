using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

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
