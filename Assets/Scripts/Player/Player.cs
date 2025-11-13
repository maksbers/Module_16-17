using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 15f;
    private float _rotationSpeed = 500f;

    private InputController _inputController;
    private MoveController _moveController;

    private Vector3 _inputAxis;


    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void FixedUpdate()
    {
        _moveController.MoveToward(_inputAxis, _speed);
        _moveController.RotateToward(_inputAxis, _rotationSpeed);
    }

    private void UpdateInput()
    {
        if (_inputController == null)
            return;

        _inputAxis = _inputController.ReadInput();
    }
}
