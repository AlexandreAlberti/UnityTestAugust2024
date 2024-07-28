using UnityEngine;

public class RegularBlockActivator : MonoBehaviour {
    private const string HIT_REGULAR_BLOCK = "HitRegularBlock";
    private const string BREAK_REGULAR_BLOCK = "BreakRegularBlock";
    private const string HIT_AND_SPAWN_ITEM_REGULAR_BLOCK = "HitAndSpawnItemRegularBlock";

    [SerializeField] private Animator _animator;
    [SerializeField] private ItemInBox _itemInBox;

    private void OnCollisionEnter2D(Collision2D collision) {
        Mario mario = collision.transform.GetComponent<Mario>();

        if (mario) {
            if (_itemInBox) {
                _itemInBox.gameObject.SetActive(true);
                _animator.SetTrigger(HIT_AND_SPAWN_ITEM_REGULAR_BLOCK);
            }
            else if (MarioManager.Instance.CanBreakRegularBlocks()) {
                _animator.SetTrigger(BREAK_REGULAR_BLOCK);
            } else {
                _animator.SetTrigger(HIT_REGULAR_BLOCK);
            }
        }
    }
}
