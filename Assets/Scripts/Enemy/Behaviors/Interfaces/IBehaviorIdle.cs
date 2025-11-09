using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorIdle
{
    void Init(Enemy owner);

    void RunIdle();
}
