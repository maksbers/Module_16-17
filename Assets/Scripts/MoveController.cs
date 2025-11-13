using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float _deadZone = 0.1f;

    private Rigidbody _rigidBody;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private Vector3 GetDirectionTo(Vector3 target) => target - transform.position;

    public void MoveToPoint(Vector3 point, float speed)
    {
        Vector3 direction = GetDirectionTo(point);
        MoveToward(direction, speed);
    }

    public void MoveToward(Vector3 direction, float speed)
    {
        direction.y = 0f;
        Vector3 normaliseDirection = direction.normalized;

        float step = speed * Time.fixedDeltaTime;

        Vector3 moveVector = normaliseDirection * step;
        Vector3 targetPosition = transform.position + moveVector;

        _rigidBody.MovePosition(targetPosition);
    }

    public void RotateToPoint(Vector3 point, float speed)
    {
        Vector3 direction = GetDirectionTo(point);
        RotateToward(direction, speed);
    }

    public void RotateToward(Vector3 direction, float speed)
    {
        if (direction.magnitude < _deadZone)
            return;

        direction.y = 0f;
        Vector3 normaliseDirection = direction.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(normaliseDirection);
        float step = speed * Time.fixedDeltaTime;
        Quaternion newRotation = Quaternion.RotateTowards(_rigidBody.rotation, targetRotation, step);

        _rigidBody.MoveRotation(newRotation);
    }
}
