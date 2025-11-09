using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorReaction
{
    void Init(Enemy owner);

    void RunReaction();
}
