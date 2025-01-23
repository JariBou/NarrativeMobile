using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TestTouchScreen : MonoBehaviour
{
    [SerializeField] private GameObject touchGameObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        touchGameObject.transform.position = new Vector3(touchWorldPosition.x, touchWorldPosition.y, 0);
    }
}
