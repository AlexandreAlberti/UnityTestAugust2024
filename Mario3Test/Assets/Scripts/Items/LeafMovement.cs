using System;
using UnityEngine;

namespace Items {
    public class LeafMovement : MonoBehaviour {

        private const string START_ANIMATION = "StartAnimation";

        [SerializeField] private float _fallSpeed;
        [SerializeField] private Leaf _leaf;
        [SerializeField] private Animator _leafAnimator;

        private bool _isFallingEnabled;

        private void Awake() {
            _isFallingEnabled = false;
            _leaf.OnStartMoving += Initialise;
        }

        private void Update() {
            if (!_isFallingEnabled) {
                return;
            }

            transform.position += Vector3.down * (_fallSpeed * Time.deltaTime);

        }

        private void Initialise(object o, EventArgs e) {
            _isFallingEnabled = true;
            _leaf.OnStartMoving -= Initialise;
            _leafAnimator.SetTrigger(START_ANIMATION);
        }

    }
}