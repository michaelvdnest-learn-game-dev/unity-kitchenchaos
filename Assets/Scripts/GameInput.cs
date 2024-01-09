using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void Start() {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    public Vector2 GetMovementVectorNormalized() {

        if (playerInputActions == null) {
            playerInputActions = new PlayerInputActions();
            Start();
        }

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalize so that player moves at the same speed in a diagonal
        inputVector = inputVector.normalized;

        return inputVector;

    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

}
