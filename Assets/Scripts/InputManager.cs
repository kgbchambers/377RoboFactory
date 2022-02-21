
/*
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private PlayerInput touchControls;

    private void Awake()
    {
        touchControls = new PlayerInput();
    }


    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPosition.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPosition.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started: " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if(OnStartTouch != null)
        {
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float) context.startTime);
        }
    }


    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended: " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnEndTouch != null)
        {
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

}
*/