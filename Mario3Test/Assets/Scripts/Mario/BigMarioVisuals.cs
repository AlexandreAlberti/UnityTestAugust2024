namespace Mario {
    public class BigMarioVisuals : MarioVisuals {
        private const string MARIO_GROW_UP = "GrowUp";

        private bool isGrowUpAnimationPending;

        private void Awake() {
            SubsribeToAll();
            isGrowUpAnimationPending = false;
        }

        private void Update() {
            if (isGrowUpAnimationPending) {
                _animator.SetTrigger(MARIO_GROW_UP);
                isGrowUpAnimationPending = false;
            }
        }

        public void MarkAsGrow() {
            isGrowUpAnimationPending = true;
        }
    }
}