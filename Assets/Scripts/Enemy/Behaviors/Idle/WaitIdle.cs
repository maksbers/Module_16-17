using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitIdle : MonoBehaviour, IBehaviorIdle
{
    private Enemy _owner;

    public void Init(Enemy owner)
    {
        _owner = owner;
    }

    public void RunIdle()
    {

    }
}
