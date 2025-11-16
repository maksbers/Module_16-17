using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed { get; private set; } = 6f;
    public float SpeedRotation { get; private set; } = 500f;
    public float MinDistanceToTarget { get; private set; } = 0.1f;
    public float LimitPosition { get; private set; } = 500f;
    public float GizmoLength { get; private set; } = 4f;

    public Transform Target => _target;

    private IBehavior _behaviorIdle;
    private IBehavior _behaviorReaction;
    private IBehavior _currentBehavior;

    private Transform _target;
    private bool _isTargetDetected = false;


    public void Init(IBehavior behaviorIdle, IBehavior behaviorReaction)
    {
        _behaviorIdle = behaviorIdle;
        _behaviorReaction = behaviorReaction;

        _currentBehavior = _behaviorIdle;
        _currentBehavior.Enter();
    }

    private void FixedUpdate()
    {
        if (_currentBehavior == null)
            return;

        UpdateBehavior();

        _currentBehavior.Update();
    }

    private void UpdateBehavior()
    {
        if (_isTargetDetected == true && _currentBehavior != _behaviorReaction)
            SwitchBehaviour(_behaviorReaction);
        else if (_isTargetDetected == false && _currentBehavior != _behaviorIdle)
            SwitchBehaviour(_behaviorIdle);
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

    private void SwitchBehaviour(IBehavior behaviour)
    {
        _currentBehavior.Exit();
        _currentBehavior = behaviour;
        _currentBehavior.Enter();
    }
}
