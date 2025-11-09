using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour, IBehaviorReaction
{
    private Enemy _owner;

    public void Init(Enemy owner)
    {
        _owner = owner;
    }

    public void RunReaction()
    {
        if (_owner == null || _owner.Player == null || _owner.IsPlayerDetected == false)
            return;

        Vector3 direction = (_owner.Player.position - _owner.transform.position).normalized;
        Vector3 opposite = -direction;

        _owner.MoveTo(opposite);

        Vector3 lookPoint = _owner.transform.position + opposite;
        _owner.RotateTo(lookPoint, _owner.Speed);
    }
}
