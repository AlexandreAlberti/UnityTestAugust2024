using Cinemachine;
using System;
using UnityEngine;

public class MarioManager : MonoBehaviour {
    public static MarioManager Instance;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private MarioVisuals _smallMario;
    [SerializeField] private BigMarioVisuals _bigMario;
    [SerializeField] private TanookiMarioVisuals _tanookiMario;
    [SerializeField] private MarioState _marioState;

    private void Awake() {
        Instance = this;
        UpdateActiveMario();
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
            UpdateActiveMario();
            _bigMario.transform.position = _smallMario.transform.position;
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
        UpdateActiveMario();
        _tanookiMario.transform.position = currentMarioVisuals.transform.position;
        _tanookiMario.MarkAsTransform();
    }
    
    private void UpdateActiveMario() {
        if (_marioState == MarioState.LittleMario) {
            _bigMario.gameObject.SetActive(false);
            _tanookiMario.gameObject.SetActive(false);
            _smallMario.gameObject.SetActive(true);
            _virtualCamera.Follow = _smallMario.transform;
        } else if (_marioState == MarioState.BigMario) {
            _bigMario.gameObject.SetActive(true);
            _tanookiMario.gameObject.SetActive(false);
            _smallMario.gameObject.SetActive(false);
            _virtualCamera.Follow = _bigMario.transform;
        } else if (_marioState == MarioState.RacoonMario) {
            _bigMario.gameObject.SetActive(false);
            _tanookiMario.gameObject.SetActive(true);
            _smallMario.gameObject.SetActive(false);
            _virtualCamera.Follow = _tanookiMario.transform;
        }
    }
}
