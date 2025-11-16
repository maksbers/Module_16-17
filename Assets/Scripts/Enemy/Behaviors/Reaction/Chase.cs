using UnityEngine;

public class Chase : IBehavior
{
    private float _speed = 6f;
    private float _speedRotation = 500f;

    private Transform _source;
    private Enemy _ownerEnemy;

    private MoveController _moveController;

    private bool _isActive = false;

    public Chase(Transform source)
    {
        _source = source;

        _moveController = _source.GetComponent<MoveController>();
        _ownerEnemy = _source.GetComponent<Enemy>();
    }

    public void Enter()
    {
        if (_isActive == true || _ownerEnemy.Target == null)
            return;

        _isActive = true;
    }

    public void Update()
    {
        if (_isActive == false)
            return;

        if (_ownerEnemy.Target == null)
        {
            Exit();
            return;
        }

        Vector3 targetPoint = _ownerEnemy.Target.position;

        _moveController.MoveToPoint(targetPoint, _speed);
        _moveController.RotateToPoint(targetPoint, _speedRotation);
    }

    public void Exit()
    {
        _isActive = false;
        _ownerEnemy.ResetDetection();
    }
}
