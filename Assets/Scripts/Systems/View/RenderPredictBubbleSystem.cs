
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
        return entity.hasBubbleNumber && entity.isPredictBubble;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            if (e.hasBubbleSlotPos && e.bubbleSlotPos.Value.x != -1)
            {
                e.bubbleView.Value.gameObject.SetActive(true);
                e.bubbleView.Value.Animation.Play("BubbleInitAnim");
                e.bubbleView.Value.Image.color = new Color(1, 1, 1, 0.5f);
                e.bubbleView.Value.TrailEffect.SetActive(false);
            }
            else
            {
                e.bubbleView.Value.gameObject.SetActive(false);
            }
        }
        
    }

}
