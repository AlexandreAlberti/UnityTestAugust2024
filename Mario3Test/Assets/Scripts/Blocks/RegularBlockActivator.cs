using Items;
using Mario;
using UnityEngine;

namespace Blocks {
    public class RegularBlockActivator : MonoBehaviour {
        private const string HIT_REGULAR_BLOCK = "HitRegularBlock";
        private const string BREAK_REGULAR_BLOCK = "BreakRegularBlock";
        private const string HIT_AND_SPAWN_ITEM_REGULAR_BLOCK = "HitAndSpawnItemRegularBlock";

        [SerializeField] private Animator _animator;
        [SerializeField] private ItemInBox _itemInBox;
        [SerializeField] private bool _isLateral;

        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isLateral) {
                return;
            }

            Mario.Mario mario = collision.transform.GetComponent<Mario.Mario>();

            if (mario) {
                OnValidContact();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_isLateral) {
                return;
            }

            TanookiTail tanookiTail = collision.transform.GetComponent<TanookiTail>();

            if (tanookiTail) {
                OnValidContact();
            }
        }

        private void OnValidContact() {
            if (_itemInBox) {
                _itemInBox.gameObject.SetActive(true);
                _animator.SetTrigger(HIT_AND_SPAWN_ITEM_REGULAR_BLOCK);
            } else if (MarioManager.Instance.CanBreakRegularBlocks()) {
                _animator.SetTrigger(BREAK_REGULAR_BLOCK);
            } else {
                _animator.SetTrigger(HIT_REGULAR_BLOCK);
            }
        }
    }
}