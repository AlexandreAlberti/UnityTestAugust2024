using Items;
using Mario;
using UnityEngine;

namespace Blocks {
    public class SurpriseBlockMultipleActivator : SurpriseBlockActivator {

        [SerializeField] private ItemInBox _itemInBoxLittleMario;

        protected override void OnValidContact() {
            if (MarioManager.Instance.CanBreakRegularBlocks()) {
                _itemInBox.gameObject.SetActive(true);
            } else {
                _itemInBoxLittleMario.gameObject.SetActive(true);
            }

            ActivateAnimator();
        }
    }
}