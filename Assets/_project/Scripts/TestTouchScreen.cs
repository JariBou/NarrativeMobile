using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TestTouchScreen : MonoBehaviour
{
    
    [SerializeField] private float _touchRadius;
    
    [Header("Developper")]
    [SerializeField] private GameObject touchGameObject;
    [SerializeField] private LayerMask clickDetectionMask;

    public void Test(InputAction.CallbackContext callbackContext)
    {
        TouchState touchState = callbackContext.ReadValue<TouchState>();
        if (touchState.phase == TouchPhase.Began)
        {
            touchGameObject.SetActive(true);
        }
        else if (touchState.phase == TouchPhase.Ended)
        {
            touchGameObject.SetActive(false);
        }

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchState.position);
        if (touchState.isTap)
        {
            Debug.Log("Tapping touch");
            Collider2D overlappedCollider = Physics2D.OverlapCircle(touchWorldPosition, _touchRadius, clickDetectionMask);
            if (overlappedCollider != null)
            {
                TouchDetectionZone touchDetectionZone = overlappedCollider.gameObject.GetComponent<TouchDetectionZone>();
                if (touchDetectionZone)
                {
                    touchDetectionZone.OnClick(touchState);
                }
            }
            else
            {
                Debug.Log("Touch could not be found");
            }
        }
        touchGameObject.transform.position = new Vector3(touchWorldPosition.x, touchWorldPosition.y, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(touchGameObject.transform.position, _touchRadius);
    }
}
