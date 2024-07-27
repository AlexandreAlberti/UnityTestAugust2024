using UnityEngine;

public class BlockActivator : MonoBehaviour {
    private const string BLOCK_HIT = "BlockHit";

    [SerializeField] Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision) {
        Mario mario = collision.transform.GetComponent<Mario>();

        if (mario) {
            _animator.SetTrigger(BLOCK_HIT);
        }
    }
}
