using Cinemachine;
using System;
using UnityEngine;

public class MarioManager : MonoBehaviour
{
    public static MarioManager Instance;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private MarioVisuals _smallMario;
    [SerializeField] private BigMarioVisuals _bigMario;

    private MarioState _marioState;

    private void Awake() {
        Instance = this;
        _marioState = MarioState.RacoonMario;
    }

    public bool CanBreakRegularBlocks() {
        return _marioState != MarioState.LittleMario;
    }

    public void MushroomPowerUp() {
        if (_marioState == MarioState.LittleMario) {
            _marioState = MarioState.BigMario;
            _virtualCamera.Follow = _bigMario.transform;
            _bigMario.transform.position = _smallMario.transform.position;
            _bigMario.gameObject.SetActive(true);
            _smallMario.gameObject.SetActive(false);
            _bigMario.MarkAsGrow();
        }
    }
}
