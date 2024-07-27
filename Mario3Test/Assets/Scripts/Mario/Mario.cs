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

    private MarioState marioState;

    private void Awake() {
        marioState = MarioState.BigMario;   
    }

    public bool CanBreakRegularBlocks() {
        return marioState != MarioState.LittleMario;
    }
}
