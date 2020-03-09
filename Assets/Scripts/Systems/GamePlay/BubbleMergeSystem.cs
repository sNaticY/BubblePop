using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubbleMergeSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentMergeNumber);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entites)
    {
        var mergeables = _gameContext.GetEntitiesWithReadyToMerge(_gameContext.currentMergeNumber.Value);
        foreach (var e in mergeables)
        {
            if (e.bubbleNumber.Value == _gameContext.currentMergeNumber.Value)
            {
                Debug.Log($"Bubble {e.bubbleNumber.Value} in {e.bubbleSlotPos.Value} merge to target {e.readyToMerge.TargetSlot} for {e.readyToMerge.TargetNumber}");
                var targetEntity = _gameContext.GetEntityWithBubbleSlotPos(e.readyToMerge.TargetSlot);
                e.isInSlotBubble = false;
                e.ReplaceBubbleTargetPos(targetEntity.position.Value);
                e.ReplaceSpeed(_gameContext.settings.Value.BubbleMergeSpeed);
            }
        }
    }
}
