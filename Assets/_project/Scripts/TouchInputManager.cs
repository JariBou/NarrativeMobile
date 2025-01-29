using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchInputManager : MonoBehaviour
{
    [Header("Touch Input Options")]
    [SerializeField] private float _touchRadius;
    
    [SerializeField] private float _minSwipeDistance = 100;
    [SerializeField] private float _minSwipeTime = 0.5f;
    
    [Header("Developper")]
    [SerializeField] private LayerMask clickDetectionMask;
    
    //DraggableElement
    private Vector3 _posDifference;
    private DraggableElement _draggableElement;
    
    private float _touchStartTime;
    
    private bool _isCheckingForTouch = true;
    
    public void SetCheckingTouchDetection(bool isCheckingForTouch) => _isCheckingForTouch = isCheckingForTouch;

    public void OnTouch(InputAction.CallbackContext callbackContext)
    {
        if (!_isCheckingForTouch) return;
        TouchState touchState = callbackContext.ReadValue<TouchState>();

        if (_touchStartTime <= 0 && touchState.phase == TouchPhase.Began)
        {
            _touchStartTime = Time.time;
        }

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchState.position);
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(touchWorldPosition, _touchRadius, clickDetectionMask);
        
        //Check if touch is Tap (short touch)
        if (touchState.isTap)
        {
            //Check for touchDetectionZone
            if (overlappedColliders.Length > 0)
            {
                foreach (Collider2D overlappedCollider in overlappedColliders)
                {
                    TouchDetectionZone touchDetectionZone = overlappedCollider.gameObject.GetComponent<TouchDetectionZone>();
                    if (touchDetectionZone)
                    {
                        touchDetectionZone.OnClick(touchState);
                    }
                }
            }
        }
        else
        {
            //Check for draggable element
            if (overlappedColliders.Length > 0)
            {
                DraggableElement draggableElement = null;
                foreach (Collider2D overlappedCollider in overlappedColliders)
                {
                    draggableElement = overlappedCollider.gameObject.GetComponent<DraggableElement>();
                    if (draggableElement) break;
                }
                if (draggableElement)
                {
                    if (touchState.phase == TouchPhase.Began)
                    {
                        //Set posDifference (more smooth grab)
                        if (draggableElement.CanBeDragged())
                        {
                            _posDifference = draggableElement.transform.position - touchWorldPosition;
                            _draggableElement = draggableElement;
                        }
                    }
                    else if (touchState.phase == TouchPhase.Ended)
                    {
                        if (_draggableElement!=null) _draggableElement.Drop();
                        _draggableElement = null;
                        return;
                    }
                }
            }
        }

        if (_draggableElement != null && touchState.phase != TouchPhase.Ended)
        {
            _draggableElement.transform.position = touchWorldPosition + _posDifference;
        }
        else if (_draggableElement == null && TouchPhase.Ended == touchState.phase && _touchStartTime > 0)
        {
            float xposDiff = touchState.position.x - touchState.startPosition.x;
            if (Mathf.Abs(xposDiff) > _minSwipeDistance && (Time.time - _touchStartTime) < _minSwipeTime)
            {
                if (xposDiff > 0)
                {
                    if (SceneManager.Instance != null) SceneManager.Instance.MoveScene(true);
                    Debug.Log("Swipe Right");
                }
                else
                {
                    if (SceneManager.Instance != null) SceneManager.Instance.MoveScene(false);
                    Debug.Log("Swipe Left");
                }
            }

            _touchStartTime = -1;
        }
    }
}
