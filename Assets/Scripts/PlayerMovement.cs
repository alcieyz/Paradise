using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _moveConstant = 1f;

    [SerializeField] private bool _isMovingForward;
    [SerializeField] private bool _isMovingBackward;
    [SerializeField] private bool _isMovingLeft;
    [SerializeField] private bool _isMovingRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _isMovingForward = Input.GetKey(KeyCode.W);
        _isMovingBackward = Input.GetKey(KeyCode.S);
        _isMovingLeft = Input.GetKey(KeyCode.A);
        _isMovingRight = Input.GetKey(KeyCode.D);


        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3();

        if (_isMovingForward)
            movement.y += _moveConstant;

        if (_isMovingBackward)
            movement.y -= _moveConstant;

        if (_isMovingLeft)
            movement.x -= _moveConstant;

        if (_isMovingRight)
            movement.x += _moveConstant;

        movement = movement.normalized * _moveSpeed * Time.deltaTime;

        transform.Translate(movement);

    }
}


