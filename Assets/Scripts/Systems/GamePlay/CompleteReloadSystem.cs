
using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

public class CompleteReloadSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public CompleteReloadSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CompleteMove, GameMatcher.WaitingForLaunch));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCompleteMove;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isCompleteMove = false;
            _gameContext.ReplaceGameState(GameState.Fire);
            
        }
    }
}
