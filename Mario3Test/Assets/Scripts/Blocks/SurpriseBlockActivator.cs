using UnityEngine;

public class SurpriseBlockActivator : MonoBehaviour {
    private const string BLOCK_HIT = "BlockHit";

    [SerializeField] private Animator _animator;
    [SerializeField] private ItemInBox _itemInBox;

    private void OnCollisionEnter2D(Collision2D collision) {
        Mario mario = collision.transform.GetComponent<Mario>();

        if (mario) {
            _itemInBox.gameObject.SetActive(true);
            _animator.SetTrigger(BLOCK_HIT);
        }
    }
}
