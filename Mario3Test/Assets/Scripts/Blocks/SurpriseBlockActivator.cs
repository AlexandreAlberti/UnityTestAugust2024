using UnityEngine;

public class SurpriseBlockActivator : MonoBehaviour {
    private const string BLOCK_HIT = "BlockHit";

    [SerializeField] private Animator _animator;
    [SerializeField] private ItemInBox _itemInBox;
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

    private void OnValidContact() {
        _itemInBox.gameObject.SetActive(true);
        _animator.SetTrigger(BLOCK_HIT);
    }
}
