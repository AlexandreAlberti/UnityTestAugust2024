using Items;
using Mario;
using UnityEngine;

namespace Blocks {
    public class SurpriseBlockMultipleActivated : SurpriseBlockActivated {

        [SerializeField] private BoxedItemSO _boxedItemLittleMarioSO;
        [SerializeField] protected ItemInBox _itemInBoxLittleMario;

        public override void OnHitAnimationEvent() {
            if (MarioManager.Instance.CanBreakRegularBlocks()) {
                ActivateProperAnimator(_boxedItemSO);
                _itemInBox.gameObject.SetActive(true);
            } else {
                ActivateProperAnimator(_boxedItemLittleMarioSO);
                _itemInBoxLittleMario.gameObject.SetActive(true);
            }
        }
    }
}