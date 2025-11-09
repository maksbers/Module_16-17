using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WalkIdle : MonoBehaviour, IBehaviorIdle
{
    private float _cooldownDirection = 1f;

    private Vector3 _targetDirection;
    private float _timer;

    private Enemy _owner;

    public void Init(Enemy owner)
    {
        _owner = owner;
    }

    public void RunIdle()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        _timer += Time.deltaTime;

        if (_timer > _cooldownDirection || _targetDirection == Vector3.zero)
        {
            _timer = 0;
            _targetDirection = GenerateDirection();
        }

        _owner.MoveTo(_targetDirection);

        Vector3 lookPoint = _owner.transform.position + _targetDirection;
        _owner.RotateTo(lookPoint, _owner.Speed);

        ProcessLimits();
    }

    private void ProcessLimits()
    {
        Vector3 position = transform.position;

        if (Mathf.Abs(position.x) > _owner.LimitPosition || Mathf.Abs(position.z) > _owner.LimitPosition)
        {
            _targetDirection = -_targetDirection;
        }
    }

    private Vector3 GenerateDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 normalizedDirection = new Vector3(randomX, 0, randomZ).normalized;

        return normalizedDirection;
    }
}
