using Cinemachine;
using System;
using UnityEngine;

public class MarioManager : MonoBehaviour {
    public static MarioManager Instance;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private MarioVisuals _smallMario;
    [SerializeField] private BigMarioVisuals _bigMario;
    [SerializeField] private TanookiMarioVisuals _tanookiMario;

    private MarioState _marioState;

    private void Awake() {
        Instance = this;
        _marioState = MarioState.RacoonMario;
    }

    public bool CanBreakRegularBlocks() {
        return _marioState != MarioState.LittleMario;
    }

    public bool CanTransformToTanooki() {
        return _marioState != MarioState.RacoonMario;
    }

    public void MushroomPowerUp() {
        if (_marioState == MarioState.LittleMario) {
            _marioState = MarioState.BigMario;
            _virtualCamera.Follow = _bigMario.transform;
            _bigMario.transform.position = _smallMario.transform.position;
            _bigMario.gameObject.SetActive(true);
            _tanookiMario.gameObject.SetActive(false);
            _smallMario.gameObject.SetActive(false);
            _bigMario.MarkAsGrow(); 
        }
    }

    public void LeafPowerUp() {
        MarioVisuals currentMarioVisuals;
        switch (_marioState) {
            case MarioState.LittleMario:
                currentMarioVisuals = _smallMario;
                break;
            case MarioState.BigMario:
                currentMarioVisuals = _bigMario;
                break;
            default:
                return;
        }

        _marioState = MarioState.RacoonMario;
        _virtualCamera.Follow = _tanookiMario.transform;
        _tanookiMario.transform.position = currentMarioVisuals.transform.position;
        _tanookiMario.gameObject.SetActive(true);
        _bigMario.gameObject.SetActive(false);
        _smallMario.gameObject.SetActive(false);
        _tanookiMario.MarkAsTransform();
    }
}
