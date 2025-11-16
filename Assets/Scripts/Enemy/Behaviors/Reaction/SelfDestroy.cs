using UnityEngine;

public class SelfDestroy : IBehavior
{
    private float _shrinkSpeed = 3f;

    private bool _isActivated;
    private float _currentScale;

    private Transform _source;


    public SelfDestroy(Transform source)
    {
        _source = source;
    }

    public void Enter()
    {
        _currentScale = _source.localScale.x;
        _isActivated = true;
    }

    public void Update()
    {
        if (_isActivated == false)
            return;

        RunSelfDestroy();
    }

    public void Exit()
    {

    }

    private void RunSelfDestroy()
    {
        if (_currentScale <= 0)
        {
            Object.Destroy(_source.gameObject);
            return;
        }    

        _currentScale -= _shrinkSpeed * Time.deltaTime;
        _source.transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
    }
}
