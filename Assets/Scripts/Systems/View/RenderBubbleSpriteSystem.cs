using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderBubbleColorSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public RenderBubbleColorSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.View);
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
            e.view.Image.sprite = setting.bubbleSetting.Sprite;
        }
    }
}