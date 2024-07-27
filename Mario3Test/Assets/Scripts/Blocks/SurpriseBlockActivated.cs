using UnityEngine;

public class RegularBlockActivated : MonoBehaviour {
    private const string GROW_AND_STAY = "GrowAndStay";
    private const string JUMP_AND_AUTO_COLLECT = "JumpAndAutoCollect";

    [SerializeField] private Animator _innerObjectAnimator;
    [SerializeField] private BoxedItemSO _boxedItemSO;
    public void OnHitAnimationEvent() {
        switch (_boxedItemSO._behaviour) {
            case BoxedItemAppearBehaviour.GrowAndStay:
                _innerObjectAnimator.SetTrigger(GROW_AND_STAY);
                break;
            case BoxedItemAppearBehaviour.JumpAndAutoCollect:
                _innerObjectAnimator.SetTrigger(JUMP_AND_AUTO_COLLECT);
                break;
            case BoxedItemAppearBehaviour.None:
            default:
                break;
        }
    }


}
