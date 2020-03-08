
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
        Debug.Log(entites.Count);
        var mergeables = _gameContext.GetEntitiesWithReadyToMerge(_gameContext.currentMergeNumber.Value);
        foreach (var e in mergeables)
        {
            if (e.bubbleNumber.Value == _gameContext.currentMergeNumber.Value)
            {
                var targetEntity = _gameContext.GetEntityWithBubbleSlotPos(e.readyToMerge.TargetSlot);
                e.ReplaceBubbleTargetPos(targetEntity.position.Value);
            }
        }
    }
}