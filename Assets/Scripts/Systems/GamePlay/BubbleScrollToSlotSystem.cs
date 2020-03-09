
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleScrollToSlotSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public BubbleScrollToSlotSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleScrollToSlot);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var posX = _gameContext.settings.Value.BubbleSize / 2 * entity.bubbleSlotPos.Value.x;
            var posY = - _gameContext.settings.Value.BubbleLineSpace * entity.bubbleSlotPos.Value.y;
            entity.ReplaceSpeed(_gameContext.settings.Value.BubblePrepareLaunchSpeed);
            entity.ReplaceBubbleTargetPos(new Vector2(posX, posY));
        }
    }
}
