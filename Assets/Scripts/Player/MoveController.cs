using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float _deadZone = 0.1f;

    private InputController _inputController;
    private Player _player;
    private Rigidbody _rigidBody;

    private Vector3 _inputAxis;


    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidBody = GetComponent<Rigidbody>();
        _inputController = GetComponent<InputController>();
    }

    private void Update()
    {
        if (_inputController == null)
            return;

        _inputAxis = _inputController.ReadInput();
    }

    private void FixedUpdate()
    {
        if (_inputAxis.magnitude < _deadZone)
            return;

        MoveTo(_inputAxis);
        RotateTo(_inputAxis);
    }

    private void MoveTo(Vector3 direction)
    {
        Vector3 moveVector = direction.normalized * _player.Speed * Time.deltaTime;

        Vector3 targetPosition = _rigidBody.position + moveVector;

        _rigidBody.MovePosition(targetPosition);
    }

    private void RotateTo(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

        Quaternion newRotation = Quaternion.Slerp(_rigidBody.rotation, targetRotation, _player.RotationSpeed * Time.deltaTime);

        _rigidBody.MoveRotation(newRotation);
    }
}
