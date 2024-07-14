using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioInputControl : MonoBehaviour
{
    public event EventHandler OnJumpAction;
    public event EventHandler OnJumpReleasedAction;
    public event EventHandler OnRunAttackAction;
    public event EventHandler OnRunReleasedAction;

    private SuperMario3Controls superMario3Controls;

    private void Awake() {
        superMario3Controls = new SuperMario3Controls();
        superMario3Controls.Player.Enable();

        superMario3Controls.Player.Jump.performed += Jump;
        superMario3Controls.Player.Jump.canceled += JumpReleased;
        superMario3Controls.Player.RunAttack.performed += RunAttack;
        superMario3Controls.Player.RunAttack.canceled += RunReleased;
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void RunAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnRunAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void RunReleased(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnRunReleasedAction?.Invoke(this, EventArgs.Empty);
    }

    private void JumpReleased(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnJumpReleasedAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = superMario3Controls.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();

        return inputVector;
    }
}
