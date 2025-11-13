using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolIdle : MonoBehaviour, IBehaviorIdle
{
    private Enemy _owner;
    public List<Transform> _targetPoints;

    private MoveController _moveController;

    private int _currentIndex;


    public void Init(Enemy owner)
    {
        _owner = owner;
        _moveController = GetComponent<MoveController>();

        _currentIndex = 0;
    }

    public void RunIdle()
    {
        if (_targetPoints == null || _targetPoints.Count == 0)
            return;

        ProcessPatrol();
    }

    private void ProcessPatrol()
    {
        Transform target = _targetPoints[_currentIndex];

        _moveController.MoveToPoint(target.position, _owner.Speed);
        _moveController.RotateToPoint(target.position, _owner.SpeedRotation);

        UpdateTargetIndex(target);
    }

    public void SetTargetPoints(List<Transform> targetPoints)
    {
        _targetPoints = targetPoints;
    }

    private void UpdateTargetIndex(Transform target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < _owner.MinDistance)
        {
            _currentIndex++;

            if (_currentIndex >= _targetPoints.Count)
            {
                _currentIndex = 0;
            }
        }
    }
}
