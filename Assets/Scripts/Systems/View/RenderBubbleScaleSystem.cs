using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderBubbleScaleSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public RenderBubbleScaleSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.WaitingForLaunch, GameMatcher.NextLaunch));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isNextLaunch;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }
}