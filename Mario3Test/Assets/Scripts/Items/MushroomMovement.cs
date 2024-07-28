using System;
using UnityEngine;

public class MushroomMovement : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private Mushroom _mushroom;

    private float _currentSpeed;

    private void Awake() {
        _mushroom.OnMarioFound += Initialise;
        _currentSpeed = 0.0f;
    }

    private void Update() {
        transform.position += Vector3.right * _currentSpeed * Time.deltaTime;
    }
    public void Initialise(object ob, float marioPositionX) {
        _currentSpeed = marioPositionX < transform.position.x ? _speed : -_speed;
        _mushroom.OnMarioFound -= Initialise;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Wall wall = collision.transform.GetComponent<Wall>();

        if (wall) {
            _currentSpeed *= -1;
        }
    }

}
