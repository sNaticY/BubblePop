
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderPredictBubbleSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public RenderPredictBubbleSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.BubbleSlotPos));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleIndex && entity.isPredictBubble;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            if (e.bubbleSlotPos.Value.x != -1)
            {
                e.view.GameObject.SetActive(true);
                e.view.Image.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                e.view.GameObject.SetActive(false);
            }
        }
        
    }
}
