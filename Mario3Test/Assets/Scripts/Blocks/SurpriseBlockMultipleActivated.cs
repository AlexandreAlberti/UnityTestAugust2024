using Mario;
using UnityEngine;

namespace Blocks {
    public class RegularBlockMultipleActivated : RegularBlockActivated {

        [SerializeField] private BoxedItemSO _boxedItemLittleMarioSO;

        public override void OnHitAnimationEvent() {
            if (MarioManager.Instance.CanBreakRegularBlocks()) {
                ActivateProperAnimator(_boxedItemSO);
            } else {
                ActivateProperAnimator(_boxedItemLittleMarioSO);
            }
        }
    }
}