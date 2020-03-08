
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleExplodeSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubbleExplodeSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleNumber);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.bubbleNumber.Value >= 2048;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var e30 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.up);
            var e90 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.right);
            var e150 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.down);
            var e210 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.down);
            var e270 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.left);
            var e330 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.up);

            if (e30 != null) e30.isReadyToDestroy = true;
            if (e90 != null) e90.isReadyToDestroy = true;
            if (e150 != null) e150.isReadyToDestroy = true;
            if (e210 != null) e210.isReadyToDestroy = true;
            if (e270 != null) e270.isReadyToDestroy = true;
            if (e330 != null) e330.isReadyToDestroy = true;
        }
    }
}
