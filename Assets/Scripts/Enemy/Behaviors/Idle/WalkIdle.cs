using UnityEngine;

public class WalkIdle : IBehavior
{
    private float _speed = 6f;
    private float _rotationSpeed = 500f;

    private float _limitPosition = 15f;
    private float _cooldownChangeDirection = 1f;

    private MoveController _moveController;

    private Vector3 _targetDirection;
    private float _timer;

    private Transform _source;


    public WalkIdle(Transform source)
    {
        _source = source;
        _moveController = _source.GetComponent<MoveController>();
    }

    public void Enter()
    {
        _targetDirection = GenerateDirection();
        _timer = 0f;
    }

    public void Update()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > _cooldownChangeDirection)
        {
            _timer = 0;
            _targetDirection = GenerateDirection();
        }

        ProcessLimits();

        _moveController.MoveToward(_targetDirection, _speed);
        _moveController.RotateToward(_targetDirection, _rotationSpeed);
    }

    public void Exit()
    {
        _timer = 0;
    }

    private void ProcessLimits()
    {
        bool isOutside = Mathf.Abs(_source.position.x) >= _limitPosition || Mathf.Abs(_source.position.z) >= _limitPosition;

        if (isOutside == false)
            return;

        Vector3 directionToCenter = Vector3.zero - _source.position;
        directionToCenter.y = 0f;

        _targetDirection = directionToCenter.normalized;

        _timer = 0f;
    }

    private Vector3 GenerateDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 normalizedDirection = new Vector3(randomX, 0, randomZ).normalized;

        return normalizedDirection;
    }
}
