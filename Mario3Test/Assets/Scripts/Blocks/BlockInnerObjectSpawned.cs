using Items;
using UnityEngine;

namespace Blocks {
    public class BlockInnerObjectSpawned : MonoBehaviour {

        [SerializeField] private ItemInBox _itemInBox;
        [SerializeField] private ItemInBox _prefabToIntantiate;

        public virtual void OnSpawnItemInBox() {
            ItemInBox newItem = Instantiate(_prefabToIntantiate, transform.position, Quaternion.identity);
            _itemInBox.gameObject.SetActive(false);
            newItem.AppearFromBox();
        }

    }
}