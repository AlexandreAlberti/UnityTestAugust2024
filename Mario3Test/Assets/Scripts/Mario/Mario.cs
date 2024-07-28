using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    private enum MarioState {
        LittleMario,
        BigMario,
        FireMario,
        RacoonMario
    }

    [SerializeField] private MarioState marioState;

    public bool CanBreakRegularBlocks() {
        return marioState != MarioState.LittleMario;
    }
}
