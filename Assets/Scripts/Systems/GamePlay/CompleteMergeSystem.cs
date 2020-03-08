
using System.Collections.Generic;
using Entitas;

public class CompleteMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public CompleteMergeSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.Value == GameState.Merge;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_gameContext.GetEntitiesWithReadyToMerge(_gameContext.currentMergeNumber.Value).Count == 0)
        {
            _gameContext.ReplaceGameState(GameState.Scroll);
        }
    }
}
