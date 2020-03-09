using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleDropDownSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubbleDropDownSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.Value == GameState.Merge || entity.gameState.Value == GameState.Scroll;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        for (int i = 0; i < 12; i++)
        {
            var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(i, -1));
            if(entity != null)
                CheckConnect(entity);
        }

        var allBubbles = _gameContext.GetEntities(GameMatcher.InSlotBubble);
        foreach (var entity in allBubbles)
        {
            if (!entity.isConnectToCeil && entity.isActiveBubble)
            {
                entity.isReadyToDropDown = true;
                entity.isActiveBubble = false;
                entity.isInSlotBubble = false;
                if(entity.hasBubbleSlotPos) entity.RemovePosition();
                if(entity.hasBubbleSlotPos) entity.RemoveBubbleSlotPos();
                if(entity.hasReadyToMerge) entity.RemoveReadyToMerge();
            }

            entity.isConnectToCeil = false;
        }
    }

    private void CheckConnect(GameEntity entity)
    {
        entity.isConnectToCeil = true;
        var e30 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.up);
        var e90 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.right);
        var e150 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.down);
        var e210 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.down);
        var e270 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.left);
        var e330 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.up);
        if(e30 != null && !e30.isConnectToCeil) CheckConnect(e30);
        if(e90 != null && !e90.isConnectToCeil) CheckConnect(e90);
        if(e150 != null && !e150.isConnectToCeil) CheckConnect(e150);
        if(e210 != null && !e210.isConnectToCeil) CheckConnect(e210);
        if(e270 != null && !e270.isConnectToCeil) CheckConnect(e270);
        if(e330 != null && !e330.isConnectToCeil) CheckConnect(e330);
    }
}
