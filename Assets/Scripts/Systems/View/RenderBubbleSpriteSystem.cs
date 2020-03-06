using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderBubbleSpriteSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public RenderBubbleSpriteSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleView);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleIndex;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            var bubbleIndex = e.bubbleIndex.Value;
            var setting = _gameContext.GetEntityWithBubbleSetting(bubbleIndex);
            e.bubbleView.Image.sprite = setting.bubbleSetting.Sprite;
        }
    }
}