using UnityEngine;

public class BlockInnerObjectMultipleSpawned : BlockInnerObjectSpawned {

    [SerializeField] protected ItemInBox _itemInBoxLittleMario;
    [SerializeField] protected ItemInBox _prefabToIntantiateLittleMario;

    public override void OnSpawnItemInBox() {
        if (MarioManager.Instance.CanBreakRegularBlocks()) {
            base.OnSpawnItemInBox();
        }
        else {
            ItemInBox newItem = Instantiate(_prefabToIntantiateLittleMario, transform.position, Quaternion.identity);
            _itemInBoxLittleMario.gameObject.SetActive(false);
            newItem.AppearFromBox();
        }
    }

}
