
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
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BubbleView, GameMatcher.BubbleSlotPos));
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
                e.bubbleView.GameObject.SetActive(true);
                e.bubbleView.Image.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                e.bubbleView.GameObject.SetActive(false);
            }
        }
        
    }
}
