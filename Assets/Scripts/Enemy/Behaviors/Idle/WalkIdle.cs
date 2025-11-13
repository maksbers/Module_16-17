using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WalkIdle : MonoBehaviour, IBehaviorIdle
{
    private float _cooldownChangeDirection = 1f;

    private MoveController _moveController;
    private Enemy _owner;

    private Vector3 _targetDirection;

    private float _timer;


    public void Init(Enemy owner)
    {
        _owner = owner;
        _moveController = GetComponent<MoveController>();

        _targetDirection = GenerateDirection();
        _timer = 0f;
    }

    public void RunIdle()
    {
        ProcessMovement();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, _targetDirection * _owner.GizmoLength, Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * _owner.GizmoLength, Color.red);
    }

    private void ProcessMovement()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > _cooldownChangeDirection)
        {
            _timer = 0;
            _targetDirection = GenerateDirection();
        }

        ProcessLimits();

        _moveController.MoveToward(_targetDirection, _owner.Speed);
        _moveController.RotateToward(_targetDirection, _owner.SpeedRotation);
    }

    private void ProcessLimits()
    {
        bool isOutside = Mathf.Abs(transform.position.x) >= _owner.LimitPosition || Mathf.Abs(transform.position.z) >= _owner.LimitPosition;

        if (isOutside == false)
            return;

        Vector3 directionToCenter = Vector3.zero - transform.position;
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
