using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

public class CompleteMergeMoveSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public CompleteMergeMoveSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CompleteMove, GameMatcher.ReadyToMerge));
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
            var targetEntity = _gameContext.GetEntityWithBubbleSlotPos(e.readyToMerge.TargetSlot);
            targetEntity?.ReplaceBubbleNumber(e.readyToMerge.TargetNumber);
            _gameContext.ReplaceGameState(GameState.Merge);
            
            e.isReadyToDestroy = true;
        }
    }
}