using UnityEngine;

public class RunAway : IBehavior
{
    private float _speed = 6f;
    private float _speedRotation = 500f;
    private float _escapeOffset = 6f;
    private float _minDistanceToTarget = 0.1f;

    private Transform _source;
    private Enemy _ownerEnemy;
    private MoveController _moveController;

    private Vector3 escapePoint;
    private bool _isActive = false;


    public RunAway(Transform source)
    {
        _source = source;
        _moveController = source.GetComponent<MoveController>();
        _ownerEnemy = _source.GetComponent<Enemy>();
    }

    public void Enter()
    {
        if (_isActive == true || _ownerEnemy.Target == null)
            return;

        CalculateEscapePoint();
        _isActive = true;
    }

    public void Update()
    {
        if (_isActive == false)
            return;

        _moveController.MoveToPoint(escapePoint, _speed);
        _moveController.RotateToPoint(escapePoint, _speedRotation);

        if (IsAchieveEscapePoint())
            UpdateActiveCondition();
    }

    public void Exit()
    {
        _isActive = false;
        _ownerEnemy.ResetDetection();
    }

    private bool IsAchieveEscapePoint()
    {
        float distanceToEscapePoint = Vector3.Distance(_source.position, escapePoint);
        return distanceToEscapePoint <= _minDistanceToTarget;
    }

    private void UpdateActiveCondition()
    {
        if (_ownerEnemy.Target == null)
        {
            Exit();
        }
        else
            CalculateEscapePoint();
    }

    private void CalculateEscapePoint()
    {
        Vector3 directionFromTarget = (_source.position - _ownerEnemy.Target.position);
        directionFromTarget.Normalize();

        escapePoint = _source.position + directionFromTarget * _escapeOffset;
        escapePoint.y = 0f;
    }
}
