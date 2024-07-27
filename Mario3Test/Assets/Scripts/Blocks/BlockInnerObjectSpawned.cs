using UnityEngine;

public class BlockInnerObjectSpawned : MonoBehaviour {

    [SerializeField] private ItemInBox _itemInBox;

    public void OnSpawnedAnimationEvent() {
        _itemInBox.AppearFromBox();
    }

}
