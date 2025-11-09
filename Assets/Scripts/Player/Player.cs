using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 15f;
    private float _rotationSpeed = 20f;

    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
}
