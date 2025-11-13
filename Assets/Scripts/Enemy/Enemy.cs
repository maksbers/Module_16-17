using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed { get; private set; } = 6f;
    public float SpeedRotation { get; private set; } = 500f;
    public float MinDistanceToTarget { get; private set; } = 0.1f;
    public float LimitPosition { get; private set; } = 500f;
    public float GizmoLength { get; private set; } = 4f;

    public Transform Target => _target;

    private IBehaviorIdle _behaviorIdle;
    private IBehaviorReaction _behaviorReaction;

    private Transform _target;
    private bool _isTargetDetected = false;


    public void Init(IBehaviorIdle behaviorIdle, IBehaviorReaction behaviorReaction)
    {
        _behaviorIdle = behaviorIdle;
        _behaviorReaction = behaviorReaction;
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
