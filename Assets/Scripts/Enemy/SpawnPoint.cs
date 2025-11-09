using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _targetPoints;

    [SerializeField] private BehaviorIdleMode _behaviorIdleMode;
    [SerializeField] private BehaviorReactionMode _behaviorReactionMode;


    private void Awake()
    {
        Spawn(_behaviorIdleMode, _behaviorReactionMode, transform.position);
    }

    public void Spawn(BehaviorIdleMode idleMode, BehaviorReactionMode reactionMode, Vector3 position)
    {
        Enemy instance = Instantiate(_enemyPrefab, position, Quaternion.identity);

        IBehaviorIdle idleBehavior = CreateIdleBehavior(instance, idleMode);
        IBehaviorReaction reactionBehavior = CreateReactionBehavior(instance, reactionMode);

        instance.Init(idleBehavior, reactionBehavior);
    }

    private IBehaviorReaction CreateReactionBehavior(Enemy instance, BehaviorReactionMode reactionMode)
    {
        switch (reactionMode)
        {
            case BehaviorReactionMode.SelfDestroy:
                {
                    SelfDestroy behavior = instance.AddComponent<SelfDestroy>();
                    behavior.Init(instance);
                    return behavior;
                }
            case BehaviorReactionMode.RunAway:
                {
                    RunAway behavior = instance.AddComponent<RunAway>();
                    behavior.Init(instance);
                    return behavior;
                }
            case BehaviorReactionMode.Chase:
                {
                    Chase behavior = instance.AddComponent<Chase>();
                    behavior.Init(instance);
                    return behavior;
                }
            default:
                Debug.LogError("Unknown BehaviorReactionMode");
                return null;
        }
    }

    private IBehaviorIdle CreateIdleBehavior(Enemy instance, BehaviorIdleMode idleMode)
    {
        switch (idleMode)
        {
            case BehaviorIdleMode.Wait:
                {
                    WaitIdle behavior = instance.AddComponent<WaitIdle>();
                    behavior.Init(instance);
                    return behavior;
                }
            case BehaviorIdleMode.Walk:
                {
                    WalkIdle behavior = instance.AddComponent<WalkIdle>();
                    behavior.Init(instance);
                    return behavior;
                }
            case BehaviorIdleMode.Patrol:
                {
                    PatrolIdle behavior = instance.AddComponent<PatrolIdle>();
                    behavior.Init(instance);
                    behavior.SetTargetPoints(_targetPoints);
                    return behavior;
                }
            default:
                Debug.LogError("Unknown BehaviorIdleMode");
                return null;
        }
    }
}
