using Items;
using Mario;
using UnityEngine;

namespace Blocks {
    public class SurpriseBlockActivator : MonoBehaviour {
        private const string BLOCK_HIT = "BlockHit";

        [SerializeField] protected Animator _animator;
        [SerializeField] private bool _isLateral;

        protected bool _blockUsed;

        private void Awake() {
            _blockUsed = false;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isLateral || _blockUsed) {
                return;
            }

            Mario.Mario mario = collision.transform.GetComponent<Mario.Mario>();

            if (mario) {
                OnValidContact();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isLateral || _blockUsed) {
                return;
            }

            TanookiTail tanookiTail = collision.transform.GetComponent<TanookiTail>();

            if (tanookiTail) {
                OnValidContact();
            }
        }

        protected virtual void OnValidContact() {
            ActivateAnimator();
            _blockUsed = true;
        }

        protected void ActivateAnimator() {
            _animator.SetTrigger(BLOCK_HIT);
        }
    }
}