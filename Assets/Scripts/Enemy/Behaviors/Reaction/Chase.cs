using UnityEngine;

public class Chase : MonoBehaviour, IBehaviorReaction
{
    private Enemy _owner;
    private MoveController _moveController;

    private bool _isActive = false;

    public void Init(Enemy owner)
    {
        _owner = owner;
        _moveController = GetComponent<MoveController>();
    }

    public void RunReaction()
    {
        if (_isActive == true || _owner.Target == null)
            return;

        _isActive = true;
    }

    private void FixedUpdate()
    {
        if (_isActive == false)
            return;

        if (_owner.Target == null)
        {
            _isActive = false;
            _owner.ResetDetection();
            return;
        }

        Vector3 targetPoint = _owner.Target.position;

        _moveController.MoveToPoint(targetPoint, _owner.Speed);
        _moveController.RotateToPoint(targetPoint, _owner.SpeedRotation);

        UpdateActiveCondition();
    }

    private void UpdateActiveCondition()
    {
        if (_owner.Target == null)
        {
            _isActive = false;
            _owner.ResetDetection();
        }
    }
}
