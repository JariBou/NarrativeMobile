using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;

public class DraggableElement : MonoBehaviour
{
    [SerializeField] private bool _goBackToDefaultPos;
    [SerializeField] private bool _canBeDragged = true;
    [SerializeField] private bool _hasTargetZone = false;
    [SerializeField, ShowIf("_hasTargetZone")] private DragTargetZone _targetZone;
    private Vector3 _defaultPosition;
    
    public bool CanBeDragged() => _canBeDragged;
    
    public void AllowDragging() => _canBeDragged = true;
    public void LockDragging() => _canBeDragged = false;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    public void Drop()
    {
        if (_hasTargetZone)
        {
            if (_targetZone.GetCollider().OverlapPoint(transform.position))
            {
                _targetZone.DraggedItem(this);
            }
            else if (_goBackToDefaultPos) transform.position = _defaultPosition;
        }
        else if (_goBackToDefaultPos) transform.position = _defaultPosition;
    }
}
