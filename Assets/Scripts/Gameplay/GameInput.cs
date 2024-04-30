using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private Vector2 inputVector;
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = new Vector2 (0, 0);
        inputVector.x = playerInputActions.Player.Move.ReadValue<Vector2>().x;
        inputVector.y = playerInputActions.Player.Move.ReadValue<Vector2>().y;
        inputVector = inputVector.normalized;
    
        // Debug.Log(inputVector);
    
        return inputVector;
    }
}
