using System;
using UnityEngine;

namespace Mario {
    public class TanookiMarioMovement : MarioMovement {

        private const float FLY_IMPULSE = 15.0f;
        private const float SOFT_FALL_IMPULSE = 2.0f;
        private const float FLY_IMPULSE_COOLDOWN = 0.2f;
        private const float MAX_TIME_FLYING = 4.0f;

        public event EventHandler OnAttack;
        public event EventHandler OnPlanning;

        private bool _isFlyAllowed;
        private bool _isSoftFallAllowed;
        private bool _lastFrameJumpPressed;
        private float _flyImpulseTimer;
        private float _flyTimer;

        private void Awake() {
            _isFlyAllowed = false;
            _isSoftFallAllowed = false;
            _flyImpulseTimer = 0.0f;
            _flyTimer = 0.0f;
            _lastFrameJumpPressed = false;
            _input.OnRunAttackAction += OnAttackAction;
        }
        protected override void HandleVerticalMovement() {

            if (_rigidbody2D.velocity.y < MAX_FALL_SPEED) {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MAX_FALL_SPEED);
            }

            _isFlyAllowed = false;
            if (!_isJumping && _isJumpPressed && _rigidbody2D.velocity.y == 0.0f) {
                // REGULAR JUMP
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * JUMP_FORCE);
                _isJumping = true;
                JumpEvent();
                _jumpExtraTime = 0.0f;
            } else if (_isJumping && !_isJumpPressed) {
                // STOP EXTENDED JUMP
                _jumpExtraTime = JUMP_EXTRA_TIME_IF_BUTTON_HOLD;
                _isFlyAllowed = true;
            } else if (_isJumping && _isJumpPressed) {
                // EXTENDED JUMP
                _jumpExtraTime += Time.deltaTime;
                if (_jumpExtraTime <= JUMP_EXTRA_TIME_IF_BUTTON_HOLD) {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * JUMP_FORCE);
                } else {
                    _isFlyAllowed = true;
                }
            }

            _flyImpulseTimer += Time.deltaTime;
            _isSoftFallAllowed = _flyImpulseTimer >= FLY_IMPULSE_COOLDOWN && _rigidbody2D.velocity.y < 0.0f;
            _isFlyAllowed &= _flyImpulseTimer >= FLY_IMPULSE_COOLDOWN && _isRunningMaxSpeedEnoughTime
                && !_lastFrameJumpPressed;

            if (_isJumping) {
                _flyTimer += Time.deltaTime;
            } else {
                _flyTimer = 0.0f;
            }

            if (_isFlyAllowed && _isJumpPressed && _flyTimer < MAX_TIME_FLYING) {
                _flyImpulseTimer = 0.0f;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * FLY_IMPULSE);
            } else if (_isSoftFallAllowed && _isJumpPressed) {
                _flyImpulseTimer = 0.0f;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * SOFT_FALL_IMPULSE);
                OnPlanning?.Invoke(this, EventArgs.Empty);
            }

            _lastFrameJumpPressed = _isJumpPressed;
        }

        private void OnAttackAction(object sender, EventArgs e) {
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}