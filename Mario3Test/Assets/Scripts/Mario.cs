using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private MarioInputControl input;

    private bool isWalking;
    private bool isRuning;
    private bool isJumping;

    private void Start() {
        input.OnJumpAction += OnJumpAction;
        input.OnRunAttackAction += OnRunAttackAction;
        input.OnRunReleasedAction += OnRunReleasedAction;
    }


    private void Update() {
        HandleMovement();
    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsRuning() {
        return isRuning;
    }


    public bool IsJumping() {
        return isJumping;
    }


    private void HandleMovement() {
        Vector2 inputVector = input.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, 0.0f);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
    }
    private void OnJumpAction(object sender, EventArgs e) {
    }

    private void OnRunAttackAction(object sender, EventArgs e) {
        isRuning = true;
    }

    private void OnRunReleasedAction(object sender, EventArgs e) {
        isRuning = true;
    }

}
