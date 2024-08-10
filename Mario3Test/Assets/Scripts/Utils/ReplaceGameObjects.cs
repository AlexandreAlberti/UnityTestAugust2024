using UnityEngine;
using UnityEditor;

namespace Utils {
    public class ReplaceGameObjects : ScriptableWizard {
        public bool copyValues = true;
        public GameObject _prefabToInstantiate;
        public GameObject _replaceAllChildrenFromParent;
        public GameObject _newPrefabDestinationParent;

        [MenuItem("Custom/Replace GameObjects")]
        static void CreateWizard() {
            ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
        }

        void OnWizardCreate() {

            if (_newPrefabDestinationParent == _replaceAllChildrenFromParent) {
                Debug.LogError("Destination and From Parents are equal.");
                return;
            }

            foreach (Transform t in _replaceAllChildrenFromParent.transform) {
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(_prefabToInstantiate);
                newObject.transform.position = t.position;
                newObject.transform.rotation = t.rotation;
                DestroyImmediate(t.gameObject);
            }

        }
    }
}