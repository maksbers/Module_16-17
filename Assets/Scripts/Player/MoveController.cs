using UnityEngine;


public class MoveController : MonoBehaviour
{
    private float _deadZone = 0.1f;

    private Rigidbody _rigidBody;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveToPoint(Vector3 point, float speed)
    {
        Vector3 direction = point - _rigidBody.position;
        MoveToward(direction, speed);
    }

    public void MoveToward(Vector3 direction, float speed)
    {
        direction.y = 0f;
        direction.Normalize();

        Vector3 moveVector = direction * speed * Time.fixedDeltaTime;
        Vector3 targetPosition = _rigidBody.position + moveVector;

        _rigidBody.MovePosition(targetPosition);
    }

    public void RotateToPoint(Vector3 point, float speed)
    {
        Vector3 direction = point - _rigidBody.position;
        RotateToward(direction, speed);
    }

    public void RotateToward(Vector3 direction, float speed)
    {
        if (direction.magnitude < _deadZone)
            return;

        direction.y = 0f;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.RotateTowards(_rigidBody.rotation, targetRotation, speed * Time.fixedDeltaTime);

        _rigidBody.MoveRotation(newRotation);
    }
}
