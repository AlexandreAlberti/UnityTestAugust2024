using System;
using UnityEngine;

namespace Mario {
    public class TanookiMarioMovement : MarioMovement {

        [SerializeField] private float _flyImpulse;
        [SerializeField] private float _softFallImpulse;
        [SerializeField] private float _flyImpulseCooldown;
        [SerializeField] private float _flyTimeMax;

        public event EventHandler OnAttack;

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

            if (_rigidbody2D.velocity.y < _maxFallSpeed) {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _maxFallSpeed);
            }


            _isFlyAllowed = false;
            if (!_isJumping && _isJumpPressed) {
                // REGULAR JUMP
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _jumpForce);
                _isJumping = true;
                JumpEvent();
                _jumpExtraTime = 0.0f;
            } else if (_isJumping && !_isJumpPressed) {
                // STOP EXTENDED JUMP
                _jumpExtraTime = _jumpExtraTimeMax;
                _isFlyAllowed = true;
            } else if (_isJumping && _isJumpPressed) {
                // EXTENDED JUMP
                _jumpExtraTime += Time.deltaTime;
                if (_jumpExtraTime <= _jumpExtraTimeMax) {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _jumpForce);
                } else {
                    _isFlyAllowed = true;
                }
            }

            _flyImpulseTimer += Time.deltaTime;
            _isSoftFallAllowed = _flyImpulseTimer >= _flyImpulseCooldown && _rigidbody2D.velocity.y < 0.0f;
            _isFlyAllowed &= _flyImpulseTimer >= _flyImpulseCooldown && _isRunningMaxSpeedEnoughTime
                && !_lastFrameJumpPressed;

            if (_isJumping) {
                _flyTimer += Time.deltaTime;
            } else {
                _flyTimer = 0.0f;
            }

            if (_isFlyAllowed && _isJumpPressed && _flyTimer < _flyTimeMax) {
                _flyImpulseTimer = 0.0f;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _flyImpulse);
                Debug.Log("Flying");
            } else if (_isSoftFallAllowed && _isJumpPressed) {
                _flyImpulseTimer = 0.0f;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Vector2.up.y * _softFallImpulse);
                Debug.Log("SoftFall");
            }

            _lastFrameJumpPressed = _isJumpPressed;
        }

        private void OnAttackAction(object sender, EventArgs e) {
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}