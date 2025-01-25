using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchInputManager : MonoBehaviour
{
    
    [SerializeField] private float _touchRadius;
    
    [Header("Developper")]
    [SerializeField] private GameObject touchGameObject;
    [SerializeField] private LayerMask clickDetectionMask;
    
    //DraggableElement
    private Vector3 _posDifference;
    private DraggableElement _draggableElement;

    public void OnTouch(InputAction.CallbackContext callbackContext)
    {
        TouchState touchState = callbackContext.ReadValue<TouchState>();

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchState.position);
        Collider2D overlappedCollider = Physics2D.OverlapCircle(touchWorldPosition, _touchRadius, clickDetectionMask);
        
        //Check if touch is Tap (short touch)
        if (touchState.isTap)
        {
            //Check for touchDetectionZone
            if (overlappedCollider != null)
            {
                TouchDetectionZone touchDetectionZone = overlappedCollider.gameObject.GetComponent<TouchDetectionZone>();
                if (touchDetectionZone)
                {
                    touchDetectionZone.OnClick(touchState);
                }
            }
        }
        else
        {
            //Check for draggable element
            if (overlappedCollider != null)
            {
                DraggableElement draggableElement = overlappedCollider.gameObject.GetComponent<DraggableElement>();
                if (draggableElement)
                {
                    if (touchState.phase == TouchPhase.Began)
                    {
                        //Set posDifference (more smooth grab)
                        _posDifference = draggableElement.transform.position - touchWorldPosition;
                        _draggableElement = draggableElement;
                    }
                    else if (touchState.phase == TouchPhase.Ended)
                    {
                        _draggableElement = null;
                    }
                }
            }
        }

        if (_draggableElement != null && touchState.phase != TouchPhase.Ended)
        {
            _draggableElement.transform.position = touchWorldPosition + _posDifference;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(touchGameObject.transform.position, _touchRadius);
    }
}
