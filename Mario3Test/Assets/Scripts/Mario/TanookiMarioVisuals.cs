using UnityEngine;

public class TanookiMarioVisuals : BigMarioVisuals {
    private const string TANOOKI_TRANSFORM = "Transform";
    private const string TANOOKI_ATTACK = "Attack";
    private const float ATTACK_COOLDOWN = 1.0f;

    private bool _isTransformAnimationPending;
    private float _timeSinceLastAttack;

    private void Awake() {
        SubsribeToAll();
        if (_marioMovement is TanookiMarioMovement tanookiMarioMovement) {
            tanookiMarioMovement.OnAttack += TanookiMarioMovement_OnAttack;
        }

        _isTransformAnimationPending = false;
        _timeSinceLastAttack = 0.0f;
    }

    private void TanookiMarioMovement_OnAttack(object sender, System.EventArgs e) {

        if (_timeSinceLastAttack >= ATTACK_COOLDOWN) {
            _timeSinceLastAttack = 0.0f;
            _animator.speed = 1.0f;
            _animator.SetTrigger(TANOOKI_ATTACK);
            _canChangeAnimatorSpeed = false;
        }
    }

    private void Update() {
        if (_isTransformAnimationPending) {
            _animator.SetTrigger(TANOOKI_TRANSFORM);
            _isTransformAnimationPending = false;
            return;
        }

        _timeSinceLastAttack += Time.deltaTime;
    }

    public void MarkAsTransform() {
        _isTransformAnimationPending = true;
    }

    public void OnAttackAnimationFinished () {
        _canChangeAnimatorSpeed = true;
    }
}
