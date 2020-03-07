using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubblePosToSlotSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubblePosToSlotSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleSlotPos);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleSlotPos;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var posX = _gameContext.settings.BubbleSize / 2 * entity.bubbleSlotPos.Value.x;
            var posY = - _gameContext.settings.BubbleLineSpace * entity.bubbleSlotPos.Value.y;
            entity.ReplacePosition(new Vector2(posX, posY));
        }
        
    }
}
