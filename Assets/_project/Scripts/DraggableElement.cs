using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.Events;

public class DraggableElement : MonoBehaviour
{
    [SerializeField] private bool _goBackToDefaultPos;
    [SerializeField] private bool _canBeDragged = true;
    [SerializeField] private bool _hasTargetZone = false;
    [SerializeField, ShowIf("_hasTargetZone")] private DragTargetZone _targetZone;
    private Vector3 _defaultPosition;
    private IDraggableItemConstraint[] _constraintInterface;

    [SerializeField] private UnityEvent _onDroppedInTargetZone;
    
    public bool CanBeDragged() => _canBeDragged;
    
    public void AllowDragging() => _canBeDragged = true;
    public void LockDragging() => _canBeDragged = false;
    
    public void DestroySelf() => Destroy(gameObject);

    private void Awake()
    {
        _defaultPosition = transform.position;
        _constraintInterface = GetComponents<IDraggableItemConstraint>();
    }

    public void Drop()
    {
        if (_hasTargetZone)
        {
            if (_targetZone.GetCollider().OverlapPoint(transform.position) && CheckConstraints())
            {
                _onDroppedInTargetZone?.Invoke();
                _targetZone.DraggedItem(this);
            }
            else if (_goBackToDefaultPos) transform.position = _defaultPosition;
        }
        else if (_goBackToDefaultPos) transform.position = _defaultPosition;
    }

    private bool CheckConstraints()
    {
        if (_constraintInterface.Length <= 0) return true;
        else
        {
            foreach (IDraggableItemConstraint constraintInterface in _constraintInterface)
            {
               if (!constraintInterface.IsConstraintCompleted()) return false; 
            }
            return true;
        }
    }
}
