using Environment;
using System;
using UnityEngine;

namespace Mario {
    public class MarioMovement : MonoBehaviour {
        private const float WALK_MAX_SPEED = 5.0f;
        private const float WALK_ACCELERATION = 20.0f;
        protected const float RUN_MAX_SPEED = 12.0f;
        private const float RUN_ACCELERATION = 15.0f;
        private const float BREAK_FORCE = 10.0f;
        protected const float JUMP_FORCE = 10.0f;
        protected const float JUMP_EXTRA_TIME_IF_BUTTON_HOLD = 0.4f;
        protected const float MIN_TIME_AT_MAX_SPEED_TO_CHANGE_ANIMATION = 3.0f;
        protected const float MAX_FALL_SPEED = -20.0f;
        
        [SerializeField] protected MarioInputControl _input;
        [SerializeField] protected Rigidbody2D _rigidbody2D;

        public event EventHandler OnStop;
        public event EventHandler OnBreaking;
        public event EventHandler OnCrouching;
        public event EventHandler<bool> OnChangeDirection;
        public event EventHandler<bool> OnJumpChange;
        public event EventHandler OnWalk;
        public event EventHandler OnRun;
        public event EventHandler OnRunMaxSpeed;

        protected float _currentSpeed;
        protected float _currentMaxSpeedTime;
        protected float _jumpExtraTime;

        private bool _isWalking;
        private bool _isRuning;
        protected bool _isRunningMaxSpeedEnoughTime;
        protected bool _isJumping;
        protected bool _isJumpPressed;
        private bool _isWalkingRight;
        private bool _isBreaking;
        private bool _isCrouching;

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
            _isBreaking = false;
            _isRunningMaxSpeedEnoughTime = false;
            _currentSpeed = 0.0f;
            _jumpExtraTime = 0.0f;
            _currentMaxSpeedTime = 0.0f;
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

        protected virtual void HandleVerticalMovement() {

            if (_rigidbody2D.velocity.y < MAX_FALL_SPEED) {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MAX_FALL_SPEED);
                return;
            }

            if (_rigidbody2D.velocity.y < 0.0f) {
                // Falling, do nothing
                return;
            }

            if (!_isJumping && _isJumpPressed) {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * JUMP_FORCE);
                _isJumping = true;
                JumpEvent();
                _jumpExtraTime = 0.0f;
            } else if (_isJumping && !_isJumpPressed) {
                _jumpExtraTime = JUMP_EXTRA_TIME_IF_BUTTON_HOLD;
            } else if (_isJumping && _isJumpPressed) {
                _jumpExtraTime += Time.deltaTime;
                if (_jumpExtraTime <= JUMP_EXTRA_TIME_IF_BUTTON_HOLD) {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * JUMP_FORCE);
                }
            }
        }

        protected void JumpEvent() {
            OnJumpChange?.Invoke(this, true);
        }

        private void HandleHorizontalMovement() {
            Vector2 inputVector = _input.GetMovementVectorNormalized();
            float moveAmountY = inputVector.y;
            float moveAmountX = inputVector.x;
            _isCrouching = !_isJumping && moveAmountY < -0.5;
            _isBreaking = false;

            if (!_isCrouching && (moveAmountX > 0.0f || moveAmountX < 0.0f)) {
                _isBreaking = _isRuning && _currentSpeed > 0.0f && moveAmountX < 0.0f || _currentSpeed < 0.0f && moveAmountX > 0.0f;

                // Debug.Log($"_isJumping {_isJumping} + _isBreaking {_isBreaking} + _currentSpeed {_currentSpeed} + moveAmountX {moveAmountX}");
                if (!_isJumping || _isBreaking
                    || _currentSpeed == 0.0f
                    || moveAmountX > 0.0f && _currentSpeed < WALK_MAX_SPEED
                    || moveAmountX < 0.0f && _currentSpeed > -WALK_MAX_SPEED) {
                    _currentSpeed += moveAmountX * Time.deltaTime * (_isRuning && !_isJumping ? RUN_ACCELERATION : WALK_ACCELERATION);
                }

                _currentSpeed = _currentSpeed > 0 ?
                    Mathf.Min(_currentSpeed, RUN_MAX_SPEED) :
                    Mathf.Max(_currentSpeed, -RUN_MAX_SPEED);
            } else if (Mathf.Abs(_currentSpeed) > 0.0f) {
                // Natural Breaking
                _currentSpeed = _currentSpeed > 0 ? 
                    Mathf.Max(0.0f, _currentSpeed - Time.deltaTime * BREAK_FORCE) :
                    Mathf.Min(0.0f, _currentSpeed + Time.deltaTime * BREAK_FORCE);
            } else {
                // Stop
                _currentSpeed = 0.0f;
            }

            transform.position += Vector3.right * _currentSpeed * Time.deltaTime;
            _isWalking = moveAmountX != 0.0f;
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

        private void HandleEvents() {
            float currentSpeedAbs = Mathf.Abs(_currentSpeed);

            if (currentSpeedAbs >= RUN_MAX_SPEED) {
                _currentMaxSpeedTime += Time.deltaTime;
            } else {
                _currentMaxSpeedTime -= Time.deltaTime;
            }

            _isRunningMaxSpeedEnoughTime = false;

            if (_currentMaxSpeedTime < 0.0f) {
                _currentMaxSpeedTime = 0.0f;
            } else if (_currentMaxSpeedTime > MIN_TIME_AT_MAX_SPEED_TO_CHANGE_ANIMATION) {
                _currentMaxSpeedTime = MIN_TIME_AT_MAX_SPEED_TO_CHANGE_ANIMATION;
                _isRunningMaxSpeedEnoughTime = true;
            }


            if (_isCrouching) {
                OnCrouching?.Invoke(this, EventArgs.Empty);
            } else if (_isBreaking) {
                OnBreaking?.Invoke(this, EventArgs.Empty);
            } else if (_isRunningMaxSpeedEnoughTime) {
                OnRunMaxSpeed?.Invoke(this, EventArgs.Empty);
            } else if (currentSpeedAbs > WALK_MAX_SPEED) {
                OnRun?.Invoke(this, EventArgs.Empty);
            } else if (currentSpeedAbs > 0.01f) {
                OnWalk?.Invoke(this, EventArgs.Empty);
            } else {
                OnStop?.Invoke(this, EventArgs.Empty);
            }

            if (_currentSpeed < 0 && _isWalkingRight || _currentSpeed > 0 && !_isWalkingRight) {
                _isWalkingRight = _currentSpeed > 0;
                OnChangeDirection?.Invoke(this, _isWalkingRight);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Floor floor = collision.transform.GetComponent<Floor>();

            if (floor) {
                _isJumping = false;
                OnJumpChange?.Invoke(this, false);
            }

            Wall wall = collision.transform.GetComponent<Wall>();

            if (wall) {
                _currentSpeed = 0.0f;
            }
        }
    }
}