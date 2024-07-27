using UnityEngine;

public class BigMarioVisuals : MonoBehaviour {
    private const string MARIO_RUN = "Running";
    private const string MARIO_RUN_MAX_SPEED = "MaxSpeed";
    private const string MARIO_JUMP = "Jumping";
    private const string MARIO_BREAK = "Breaking";
    private const string MARIO_CROUCH = "Crouching";

    [SerializeField] private BigMarioMovement _bigMarioMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _bigMarioMovement.OnStop += BigMarioMovement_OnStop;
        _bigMarioMovement.OnRun += BigMarioMovement_OnRun;
        _bigMarioMovement.OnRunMaxSpeed += BigMarioMovement_OnRunMaxSpeed;
        _bigMarioMovement.OnChangeDirection += BigMarioMovement_OnChangeDirection;
        _bigMarioMovement.OnJumpChange += BigMarioMovement_OnJumpChange;
        _bigMarioMovement.OnBreaking += BigMarioMovement_OnBreaking;
        _bigMarioMovement.OnCrouching += BigMarioMovement_OnCrouching;
    }

    private void BigMarioMovement_OnBreaking(object sender, System.EventArgs e) {
        _animator.SetBool(MARIO_BREAK, true);
    }

    private void BigMarioMovement_OnCrouching(object sender, System.EventArgs e) {
        _animator.SetBool(MARIO_CROUCH, true);
    }

    private void BigMarioMovement_OnJumpChange(object sender, bool isMarioJumping) {
        _animator.SetBool(MARIO_JUMP, isMarioJumping);
        _animator.SetBool(MARIO_BREAK, false);
        _animator.SetBool(MARIO_CROUCH, false);
    }

    private void BigMarioMovement_OnRun(object sender, float animationSpeed) {
        _animator.speed = Mathf.Max(1.0f, animationSpeed);
        _animator.SetBool(MARIO_RUN, true);
        _animator.SetBool(MARIO_RUN_MAX_SPEED, false);
        _animator.SetBool(MARIO_BREAK, false);
        _animator.SetBool(MARIO_CROUCH, false);
    }

    private void BigMarioMovement_OnRunMaxSpeed(object sender, float animationSpeed) {
        _animator.speed = animationSpeed;
        _animator.SetBool(MARIO_RUN, true);
        _animator.SetBool(MARIO_RUN_MAX_SPEED, true);
        _animator.SetBool(MARIO_CROUCH, false);
    }

    private void BigMarioMovement_OnStop(object sender, System.EventArgs e) {
        _animator.SetBool(MARIO_RUN, false);
        _animator.SetBool(MARIO_RUN_MAX_SPEED, false);
        _animator.SetBool(MARIO_BREAK, false);
        _animator.SetBool(MARIO_CROUCH, false);
    }

    private void BigMarioMovement_OnChangeDirection(object sender, bool isWalkingRight) {
        _spriteRenderer.flipX = !isWalkingRight;
        _animator.SetBool(MARIO_BREAK, false);
        _animator.SetBool(MARIO_CROUCH, false);
    }
}
