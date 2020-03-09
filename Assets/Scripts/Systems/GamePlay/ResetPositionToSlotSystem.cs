using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ResetPositionToSlotSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public ResetPositionToSlotSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleSlotPos);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleSlotPos && !entity.hasBubbleScrollToSlot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var posX = _gameContext.settings.Value.BubbleSize / 2 * entity.bubbleSlotPos.Value.x;
            var posY = - _gameContext.settings.Value.BubbleLineSpace * entity.bubbleSlotPos.Value.y;
            entity.ReplacePosition(new Vector2(posX, posY));
        }
        
    }
}
