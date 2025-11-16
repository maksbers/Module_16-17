using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _targetPoints;

    [SerializeField] private EnumBehaviorIdle _behaviorIdleMode;
    [SerializeField] private EnumBehaviorReaction _behaviorReactionMode;


    private void Awake()
    {
        Spawn(_behaviorIdleMode, _behaviorReactionMode, transform.position);
    }

    public void Spawn(EnumBehaviorIdle idleMode, EnumBehaviorReaction reactionMode, Vector3 position)
    {
        Enemy instance = Instantiate(_enemyPrefab, position, Quaternion.identity);

        IBehavior idleBehavior = CreateIdleBehavior(instance.transform, idleMode);
        IBehavior reactionBehavior = CreateReactionBehavior(instance.transform, reactionMode);

        instance.Init(idleBehavior, reactionBehavior);
    }

    private IBehavior CreateReactionBehavior(Transform instance, EnumBehaviorReaction reactionMode)
    {
        switch (reactionMode)
        {
            case EnumBehaviorReaction.SelfDestroy:
                return new SelfDestroy(instance);
            case EnumBehaviorReaction.RunAway:
                return new RunAway(instance);
            case EnumBehaviorReaction.Chase:
                return new Chase(instance);
            default:
                Debug.LogError("Unknown BehaviorReactionMode");
                return null;
        }
    }

    private IBehavior CreateIdleBehavior(Transform instance, EnumBehaviorIdle idleMode)
    {
        switch (idleMode)
        {
            case EnumBehaviorIdle.Wait:
                return new WaitIdle();
            case EnumBehaviorIdle.Walk:
                return new WalkIdle(instance);
            case EnumBehaviorIdle.Patrol:
                return new PatrolIdle(instance, _targetPoints);
            default:
                Debug.LogError("Unknown BehaviorIdleMode");
                return null;
        }
    }
}
