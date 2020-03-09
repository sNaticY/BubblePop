using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

public class CompleteScrollSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public CompleteScrollSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CompleteMove, GameMatcher.BubbleScrollToSlot));
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
            e.RemoveBubbleScrollToSlot();
            e.ReplaceBubbleSlotPos(e.bubbleSlotPos.Value);
        }
        _gameContext.ReplaceGameState(GameState.Reload);
    }
}