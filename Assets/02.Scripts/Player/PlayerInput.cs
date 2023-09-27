using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> OnMovementKeyPress;

    public UnityEvent OnAttackButtonPress;
    public UnityEvent OnJumpButtonPressDown;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetMovementInput();
        GetAttackInput();
        GetJumpInput();
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector3(h, 0f, v));
    }

    private void GetAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackButtonPress?.Invoke();
        }

    }

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpButtonPressDown?.Invoke();
        }
    }

}
