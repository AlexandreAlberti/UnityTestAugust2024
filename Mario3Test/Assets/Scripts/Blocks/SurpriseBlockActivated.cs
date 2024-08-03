using UnityEngine;

public class RegularBlockActivated : MonoBehaviour {
    private const string GROW_AND_STAY = "GrowAndStay";
    private const string JUMP_AND_AUTO_COLLECT = "JumpAndAutoCollect";
    private const string JUMP_AND_GRACEFULLY_FALL= "JumpAndGracefullyFall";

    [SerializeField] private Animator _innerObjectAnimator;
    [SerializeField] protected BoxedItemSO _boxedItemSO;
    public virtual void OnHitAnimationEvent() {
        ActivateProperAnimator(_boxedItemSO);
    }

    protected void ActivateProperAnimator(BoxedItemSO boxedItemSO) {
        switch (boxedItemSO._behaviour) {
            case BoxedItemAppearBehaviour.GrowAndStay:
                _innerObjectAnimator.SetTrigger(GROW_AND_STAY);
                break;
            case BoxedItemAppearBehaviour.JumpAndAutoCollect:
                _innerObjectAnimator.SetTrigger(JUMP_AND_AUTO_COLLECT);
                break;
            case BoxedItemAppearBehaviour.JumpAndGracefullyFall:
                _innerObjectAnimator.SetTrigger(JUMP_AND_GRACEFULLY_FALL);
                break;
            case BoxedItemAppearBehaviour.None:
            default:
                break;
        }
    }

}
