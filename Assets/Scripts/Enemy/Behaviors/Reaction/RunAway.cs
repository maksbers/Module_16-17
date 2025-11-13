using UnityEngine;

public class RunAway : MonoBehaviour, IBehaviorReaction
{
    private float _escapeOffset = 6f;

    private Enemy _owner;
    private MoveController _moveController;

    private Vector3 escapePoint;
    private bool _isActive = false;

    public void Init(Enemy owner)
    {
        _owner = owner;
        _moveController = GetComponent<MoveController>();
    }

    public void RunReaction()
    {
        if (_isActive == true)
            return;

        CalculateEscapePoint();
        _isActive = true;
    }

    private void FixedUpdate()
    {
        if (_isActive == false)
            return;

        _moveController.MoveToPoint(escapePoint, _owner.Speed);
        _moveController.RotateToPoint(escapePoint, _owner.SpeedRotation);

        if (IsAchieveEscapePoint())
            UpdateActiveCondition();
    }

    private bool IsAchieveEscapePoint()
    {
        float distanceToEscapePoint = Vector3.Distance(transform.position, escapePoint);
        return distanceToEscapePoint <= _owner.MinDistanceToTarget;
    }

    private void UpdateActiveCondition()
    {
        if (_owner.Target == null)
        {
            _isActive = false;
            _owner.ResetDetection();
        }
        else
            CalculateEscapePoint();
    }

    private void CalculateEscapePoint()
    {
        Vector3 directionFromTarget = (transform.position - _owner.Target.position);
        directionFromTarget.Normalize();

        escapePoint = transform.position + directionFromTarget * _escapeOffset;
        escapePoint.y = 0f;
    }

    private void OnDrawGizmos()
    {
        if (_isActive == true)
        {
            Gizmos.DrawSphere(escapePoint, 0.5f);
            Gizmos.DrawLine(transform.position, escapePoint);
        }
    }
}
