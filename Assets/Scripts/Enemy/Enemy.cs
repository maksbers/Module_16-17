using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 6f;
    private float _speedRotation = 500f;

    private float _minDistance = 0.1f;
    private float _limitPosition = 15f;
    private float _gizmoLength;

    private IBehaviorIdle _behaviorIdle;
    private IBehaviorReaction _behaviorReaction;

    private MoveController _moveController;
    private Transform _target;
    private bool _isTargetDetected = false;

    public float Speed => _speed;
    public float SpeedRotation => _speedRotation;
    public float MinDistance => _minDistance;
    public float LimitPosition => _limitPosition;
    public float GizmoLength => _gizmoLength;
    public Transform Target => _target;
    public MoveController MoveController => _moveController;
    public bool IsTargetDetected => _isTargetDetected;

    public float DistanceToPlayer;

    public void Init(IBehaviorIdle behaviorIdle, IBehaviorReaction behaviorReaction)
    {
        _behaviorIdle = behaviorIdle;
        _behaviorReaction = behaviorReaction;
    }

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }

    private void FixedUpdate()
    {
        UpdateBehavior();
    }

    private void UpdateBehavior()
    {
        if (_isTargetDetected == true)
            _behaviorReaction.RunReaction();
        else
            _behaviorIdle.RunIdle();
    }

    public void ResetDetection()
    {
        _isTargetDetected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _target = player.transform;
            _isTargetDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _target = null;
        }
    }
}
