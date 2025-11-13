using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour, IBehaviorReaction
{
    private Enemy _owner;

    private float _shrinkSpeed = 3f;

    private bool _isActivated;
    private float _currentScale;

    public void Init(Enemy owner)
    {
        _owner = owner;
    }

    public void RunReaction()
    {
        _currentScale = transform.localScale.x;
        _isActivated = true;
    }

    private void Update()
    {
        if (_isActivated == false)
            return;

        RunSelfDestroy();
    }

    private void RunSelfDestroy()
    {
        if (_currentScale <= 0)
            Destroy(gameObject);

        _currentScale -= _shrinkSpeed * Time.deltaTime;

        transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
    }
}
