using System;
using UnityEngine;

public class BigMarioMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _walkAcceleration;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _runAcceleration;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpExtraTimeMax;
    [SerializeField] private MarioInputControl _input;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public event EventHandler OnStop;
    public event EventHandler<bool> OnChangeDirection;
    public event EventHandler<float> OnRun;
    public event EventHandler<float> OnRunMaxSpeed;

    private float _currentSpeed;
    private float _jumpExtraTime;

    private bool _isWalking;
    private bool _isRuning;
    private bool _isJumping;
    private bool _isJumpPressed;
    private bool _isWalkingRight;

    private void Start() {
        _input.OnJumpAction += OnJumpAction;
        _input.OnJumpReleasedAction += OnJumpReleasedAction;
        _input.OnRunAttackAction += OnRunAttackAction;
        _input.OnRunReleasedAction += OnRunReleasedAction;
        _isWalking = false;
        _isRuning = false;
        _isJumping = false;
        _isJumpPressed = false;
        _isWalkingRight = true;
        _currentSpeed = 0.0f;
        _jumpExtraTime = 0.0f;
    }


    private void Update() {
        HandleHorizontalMovement();
        HandleVerticalMovement();
        HandleEvents();
    }

    public bool IsWalking() {
        return _isWalking;
    }

    public bool IsRuning() {
        return _isRuning;
    }


    public bool IsJumping() {
        return _isJumping;
    }

    private void HandleVerticalMovement() {
        if (!_isJumping && _isJumpPressed) {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _jumpForce);
            _isJumping = true;
            _jumpExtraTime = 0.0f;
        } else if (_isJumping && _isJumpPressed) {
            _jumpExtraTime += Time.deltaTime;
            if (_jumpExtraTime <= _jumpExtraTimeMax) {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _jumpForce);
            }
        }
    }

    private void HandleHorizontalMovement() {
        Vector2 inputVector = _input.GetMovementVectorNormalized();
        float moveAmount = inputVector.x;
        
        if (moveAmount > 0.0f || moveAmount < 0.0f) {
            _currentSpeed += moveAmount * Time.deltaTime * (_isRuning ? _runAcceleration : _walkAcceleration);

            _currentSpeed = _currentSpeed > 0 ?
                Mathf.Min(_currentSpeed, _isRuning ? _runSpeed : _walkSpeed) :
                Mathf.Max(_currentSpeed, _isRuning ? -_runSpeed : -_walkSpeed);

        } else if (_currentSpeed > 0.1f) {
            _currentSpeed = Mathf.Max(0.0f, _currentSpeed - Time.deltaTime * _breakForce);

        } else if (_currentSpeed < -0.1f) {
            _currentSpeed = Mathf.Min(0.0f, _currentSpeed + Time.deltaTime * _breakForce);
        } else {
            _currentSpeed = 0.0f;
        }

        transform.position += Vector3.right * _currentSpeed * Time.deltaTime;
        _isWalking = moveAmount != 0.0f;
    }
    private void OnJumpAction(object sender, EventArgs e) {
        _isJumpPressed = true;
    }

    private void OnRunAttackAction(object sender, EventArgs e) {
        _isRuning = true;
    }

    private void OnRunReleasedAction(object sender, EventArgs e) {
        _isRuning = false;
    }

    private void OnJumpReleasedAction(object sender, EventArgs e) {
        _isJumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Floor floor = collision.transform.GetComponent<Floor>();

        if (floor) {
            _isJumping = false;
        }
    }

    private void HandleEvents() {
        float currentSpeedAbs = Mathf.Abs(_currentSpeed);

        if (currentSpeedAbs >= _runSpeed) {
            OnRunMaxSpeed?.Invoke(this, currentSpeedAbs);
        } else if (currentSpeedAbs > 0.01f) {
            OnRun?.Invoke(this, currentSpeedAbs);
        } else {
            OnStop?.Invoke(this, EventArgs.Empty);
        }

        if (_currentSpeed < 0 && _isWalkingRight || _currentSpeed > 0 && !_isWalkingRight) {
            _isWalkingRight = _currentSpeed > 0;
            OnChangeDirection?.Invoke(this, _isWalkingRight);
        }
    }

}
