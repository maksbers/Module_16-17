using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour, IBehaviorReaction
{
    private Enemy _enemy;

    private bool _isActivated;

    private float _shrinkSpeed = 3f;
    private float _minScale = 0.01f;

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void RunReaction()
    {
        if (_enemy == null)
            return;

        if (_enemy.IsPlayerDetected)
            _isActivated = true;

        if (_isActivated == false)
            return;

        float shrink = _shrinkSpeed * Time.deltaTime;

        transform.localScale -= new Vector3(shrink, shrink, shrink);

        if (transform.localScale.x <= _minScale)
        {
            Destroy(gameObject);
        }
    }
}
