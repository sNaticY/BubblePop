using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ColliderControlSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public ColliderControlSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.InSlotBubble));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleNumber;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            e.bubbleView.Value.Collider.enabled = true;
        }
    }
}