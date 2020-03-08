
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
            var e30 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.up);
            var e90 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.right);
            var e150 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.down);
            var e210 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.down);
            var e270 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.left);
            var e330 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.up);

            if (e30 != null) SetBubbleReadyToDestroy(e30);
            if (e90 != null) SetBubbleReadyToDestroy(e90);
            if (e150 != null) SetBubbleReadyToDestroy(e150);
            if (e210 != null) SetBubbleReadyToDestroy(e210);
            if (e270 != null) SetBubbleReadyToDestroy(e270);
            if (e330 != null) SetBubbleReadyToDestroy(e330);
            entity.isReadyToDestroy = true;
        }
        
        _gameContext.ReplaceGameState(GameState.Merge);
    }

    private void SetBubbleReadyToDestroy(GameEntity entity)
    {
        entity.isReadyToDestroy = true;
        if(entity.hasReadyToMerge) entity.RemoveReadyToMerge();
        if(entity.hasBubbleSlotPos) entity.RemoveBubbleSlotPos();
    }
}
