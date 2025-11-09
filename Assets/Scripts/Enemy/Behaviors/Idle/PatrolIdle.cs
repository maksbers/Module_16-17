using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolIdle : MonoBehaviour, IBehaviorIdle
{
    private Enemy _owner;
    public List<Transform> _targetPoints;

    private int _currentIndex;
    private bool _isInitialized;


    public void Init(Enemy owner)
    {
        _owner = owner;

        _currentIndex = 0;
        _isInitialized = true;
    }

    public void RunIdle()
    {
        if (!_isInitialized || _targetPoints == null || _targetPoints.Count == 0)
            return;

        ProcessPatrol();
    }

    private void ProcessPatrol()
    {
        Transform target = _targetPoints[_currentIndex];

        _owner.MoveTo(target);
        _owner.RotateTo(target.position, _owner.Speed);

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
