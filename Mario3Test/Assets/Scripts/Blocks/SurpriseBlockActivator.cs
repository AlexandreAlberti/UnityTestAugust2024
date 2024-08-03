using UnityEngine;

public class SurpriseBlockActivator : MonoBehaviour {
    private const string BLOCK_HIT = "BlockHit";

    [SerializeField] protected Animator _animator;
    [SerializeField] protected ItemInBox _itemInBox;
    [SerializeField] private bool _isLateral;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_isLateral) {
            return;
        }

        Mario mario = collision.transform.GetComponent<Mario>();

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

    protected virtual void OnValidContact() {
        _itemInBox.gameObject.SetActive(true);
        ActivateAnimator();
    }

    protected void ActivateAnimator() {
        _animator.SetTrigger(BLOCK_HIT);
    }
}
