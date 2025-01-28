using System;
using UnityEngine;

public class DraggableElement : MonoBehaviour
{
    [SerializeField] private bool _goBackToDefaultPos;
    [SerializeField] private bool _canBeDragged = true;
    private Vector3 _defaultPosition;
    
    public bool CanBeDragged() => _canBeDragged;
    
    public void AllowDragging() => _canBeDragged = true;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    public void Drop()
    {
        if (_goBackToDefaultPos) transform.position = _defaultPosition;
    }
}
