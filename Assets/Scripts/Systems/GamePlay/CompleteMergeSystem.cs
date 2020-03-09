
using System.Collections.Generic;
using Entitas;
using UnityEngine;

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
        // Debug.Log("Call Merge --------- ");
        while (true)
        {
            // Debug.Log("Cur Merge Number = " + _gameContext.currentMergeNumber.Value);
            if (_gameContext.currentMergeNumber.Value >= 2048)
            {
                _gameContext.ReplaceGameState(GameState.Scroll);
                return;
            }
            var readyToMerge = _gameContext.GetEntitiesWithReadyToMerge(_gameContext.currentMergeNumber.Value).Count;
            // Debug.Log("Ready to Merge Number = " + readyToMerge);
            if (readyToMerge == 0)
            {
                // Debug.Log("Replace current Merge Number = " + _gameContext.currentMergeNumber.Value * 2);
                _gameContext.ReplaceCurrentMergeNumber(_gameContext.currentMergeNumber.Value * 2);
            }
            else
            {
                _gameContext.ReplaceGameState(GameState.Merge);
                return;
            }
        }
    }
}
