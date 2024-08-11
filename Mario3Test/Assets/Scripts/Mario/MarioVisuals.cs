using Unity.VisualScripting;
using UnityEngine;

namespace Mario {
    public class MarioVisuals : MonoBehaviour {
        private const string MARIO_WALK = "Walking";
        private const string MARIO_RUN = "Running";
        private const string MARIO_RUN_MAX_SPEED = "MaxSpeed";
        private const string MARIO_JUMP = "Jumping";
        private const string MARIO_BREAK = "Breaking";
        private const string MARIO_CROUCH = "Crouching";

        [SerializeField] protected MarioMovement _marioMovement;
        [SerializeField] protected Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake() {
            SubsribeToAll();
        }

        protected void SubsribeToAll() {
            _marioMovement.OnStop += MarioMovement_OnStop;
            _marioMovement.OnWalk += MarioMovement_OnWalk;
            _marioMovement.OnRun += MarioMovement_OnRun;
            _marioMovement.OnRunMaxSpeed += MarioMovement_OnRunMaxSpeed;
            _marioMovement.OnChangeDirection += MarioMovement_OnChangeDirection;
            _marioMovement.OnJumpChange += MarioMovement_OnJumpChange;
            _marioMovement.OnBreaking += MarioMovement_OnBreaking;
            _marioMovement.OnCrouching += MarioMovement_OnCrouching;
        }

        private void MarioMovement_OnBreaking(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_BREAK, true);
        }

        protected virtual void MarioMovement_OnCrouching(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_CROUCH, true);
        }

        private void MarioMovement_OnJumpChange(object sender, bool isMarioJumping) {
            _animator.SetBool(MARIO_JUMP, isMarioJumping);
            _animator.SetBool(MARIO_BREAK, false);
            _animator.SetBool(MARIO_CROUCH, false);
        }

        private void MarioMovement_OnWalk(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_WALK, true);
            _animator.SetBool(MARIO_RUN, false);
            _animator.SetBool(MARIO_RUN_MAX_SPEED, false);
            _animator.SetBool(MARIO_BREAK, false);
            _animator.SetBool(MARIO_CROUCH, false);
        }

        private void MarioMovement_OnRun(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_WALK, false);
            _animator.SetBool(MARIO_RUN, true);
            _animator.SetBool(MARIO_RUN_MAX_SPEED, false);
            _animator.SetBool(MARIO_BREAK, false);
            _animator.SetBool(MARIO_CROUCH, false);
        }

        private void MarioMovement_OnRunMaxSpeed(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_WALK, false);
            _animator.SetBool(MARIO_RUN, true);
            _animator.SetBool(MARIO_RUN_MAX_SPEED, true);
            _animator.SetBool(MARIO_CROUCH, false);
        }

        private void MarioMovement_OnStop(object sender, System.EventArgs e) {
            _animator.SetBool(MARIO_WALK, false);
            _animator.SetBool(MARIO_RUN, false);
            _animator.SetBool(MARIO_RUN_MAX_SPEED, false);
            _animator.SetBool(MARIO_BREAK, false);
            _animator.SetBool(MARIO_CROUCH, false);
        }

        private void MarioMovement_OnChangeDirection(object sender, bool isWalkingRight) {
            _spriteRenderer.flipX = !isWalkingRight;
            _animator.SetBool(MARIO_BREAK, false);
            _animator.SetBool(MARIO_CROUCH, false);
        }
    }
}