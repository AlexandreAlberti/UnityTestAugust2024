using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkAcceleration;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runAcceleration;
    [SerializeField] private float breakForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpExtraTimeMax;
    [SerializeField] private MarioInputControl input;
    [SerializeField] private Rigidbody2D rigidbody2D;

    private float currentSpeed;
    private float jumpExtraTime;

    private bool isWalking;
    private bool isRuning;
    private bool isJumping;
    private bool isJumpPressed;

    private void Start() {
        input.OnJumpAction += OnJumpAction;
        input.OnJumpReleasedAction += OnJumpReleasedAction;
        input.OnRunAttackAction += OnRunAttackAction;
        input.OnRunReleasedAction += OnRunReleasedAction;
        isWalking = false;
        isRuning = false;
        isJumping = false;
        isJumpPressed = false;
        currentSpeed = 0.0f;
        jumpExtraTime = 0.0f;
    }


    private void Update() {
        HandleHorizontalMovement();
        HandleVerticalMovement();
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

    private void HandleVerticalMovement() {
        if (!isJumping && isJumpPressed) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * jumpForce);
            isJumping = true;
            jumpExtraTime = 0.0f;
        } else if (isJumping && isJumpPressed) {
            jumpExtraTime += Time.deltaTime;
            if (jumpExtraTime <= jumpExtraTimeMax) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * jumpForce);
            }
        }
    }

    private void HandleHorizontalMovement() {
        Vector2 inputVector = input.GetMovementVectorNormalized();
        float moveAmount = inputVector.x;
        
        if (moveAmount > 0.0f || moveAmount < 0.0f) {
            currentSpeed += moveAmount * Time.deltaTime * (isRuning ? runAcceleration : walkAcceleration);

            currentSpeed = currentSpeed > 0 ?
                Mathf.Min(currentSpeed, isRuning ? runSpeed : walkSpeed) :
                Mathf.Max(currentSpeed, isRuning ? -runSpeed : -walkSpeed);

        } else if (currentSpeed > 0) {
            currentSpeed = Mathf.Max(0.0f, currentSpeed - Time.deltaTime * breakForce);

        } else if (currentSpeed < 0) {
            currentSpeed = Mathf.Min(0.0f, currentSpeed + Time.deltaTime * breakForce);
        }

        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        isWalking = moveAmount != 0.0f;
    }
    private void OnJumpAction(object sender, EventArgs e) {
        isJumpPressed = true;
    }

    private void OnRunAttackAction(object sender, EventArgs e) {
        isRuning = true;
    }

    private void OnRunReleasedAction(object sender, EventArgs e) {
        isRuning = false;
    }

    private void OnJumpReleasedAction(object sender, EventArgs e) {
        isJumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Floor floor = collision.transform.GetComponent<Floor>();

        if (floor) {
            isJumping = false;
        }
    }
}
