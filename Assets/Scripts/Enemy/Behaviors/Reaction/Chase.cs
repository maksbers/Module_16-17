using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour, IBehaviorReaction
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

        _owner.MoveTo(_owner.Player);
        _owner.RotateTo(_owner.Player.position, _owner.Speed);
    }
}
