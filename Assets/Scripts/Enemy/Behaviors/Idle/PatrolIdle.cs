using System.Collections.Generic;
using UnityEngine;

public class PatrolIdle : IBehavior
{
    private float _speed = 6f;
    private float _speedRotation = 500f;
    private float _minDistanceToTarget = 0.1f;

    private Transform _source;
    private List<Transform> _targetPoints;
    private MoveController _moveController;

    private int _currentIndex;


    public PatrolIdle(Transform source, List<Transform> targetPoints)
    {
        _source = source;
        _targetPoints = targetPoints;

        _moveController = _source.GetComponent<MoveController>();
    }

    public void Enter()
    {
        if (_targetPoints == null || _targetPoints.Count == 0)
            return;

        _currentIndex = 0;
    }

    public void Update()
    {
        if (_targetPoints == null || _targetPoints.Count == 0)
            return;

        ProcessPatrol();
    }

    public void Exit()
    {

    }

    private void ProcessPatrol()
    {
        Transform target = _targetPoints[_currentIndex];

        _moveController.MoveToPoint(target.position, _speed);
        _moveController.RotateToPoint(target.position, _speedRotation);

        UpdateTargetIndex(target);
    }

    private void UpdateTargetIndex(Transform target)
    {
        float distanceToTarget = Vector3.Distance(_source.position, target.position);

        if (distanceToTarget < _minDistanceToTarget)
        {
            _currentIndex++;

            if (_currentIndex >= _targetPoints.Count)
            {
                _currentIndex = 0;
            }
        }
    }
}
