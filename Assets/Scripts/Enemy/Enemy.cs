using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _minDistance = 0.1f;
    private float _limitPosition = 20f;

    private IBehaviorIdle _behaviorIdle;
    private IBehaviorReaction _behaviorReaction;

    private Transform _player;
    private bool _isPlayerDetected = false;

    public float Speed => _speed;
    public float MinDistance => _minDistance;
    public float LimitPosition => _limitPosition;
    public Transform Player => _player;
    public bool IsPlayerDetected => _isPlayerDetected;



    public void Init(IBehaviorIdle behaviorIdle, IBehaviorReaction behaviorReaction)
    {
        _behaviorIdle = behaviorIdle;
        _behaviorReaction = behaviorReaction;
    }

    private void Update()
    {
        UpdateBehavior();
    }

    private void UpdateBehavior()
    {
        _behaviorReaction.RunReaction();

        if (IsPlayerDetected == false)
            _behaviorIdle.RunIdle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _isPlayerDetected = true;
            _player = player.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _isPlayerDetected = false;
            _player = null;
        }
    }

    public void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }

    public void RotateTo(Vector3 targetPosition, float rotationSpeed)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
