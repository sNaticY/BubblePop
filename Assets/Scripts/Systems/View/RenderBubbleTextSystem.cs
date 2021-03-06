using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderBubbleTextSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public RenderBubbleTextSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BubbleView, GameMatcher.BubbleNumber));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleNumber && !entity.isPredictBubble;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            var number = e.bubbleNumber.Value;
            e.bubbleView.Value.Text.text = number >= 1000 ? (number / 1000) + "K" : number.ToString();
        }
    }
}