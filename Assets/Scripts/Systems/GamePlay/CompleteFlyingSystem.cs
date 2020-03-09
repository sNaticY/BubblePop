
using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

public class CompleteFlying : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    public CompleteFlying(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CompleteMove, GameMatcher.BubbleTargetSlot));
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
            var predictBubble = _gameContext.predictBubbleEntity;
            if (predictBubble.bubbleSlotPos.Value == e.bubbleTargetSlot.TargetSlot)
            {
                predictBubble.RemoveBubbleSlotPos();
            }
            e.AddBubbleSlotPos(e.bubbleTargetSlot.TargetSlot);
            e.isInSlotBubble = true;
            e.isActiveBubble = true;
            e.isNewBubble = true;
            e.bubbleView.Value.TrailEffect.SetActive(false);
            e.RemoveBubbleTargetSlot();
            
            _gameContext.CreateEntity().ReplacePlayAudio(AudioType.Bubble);
            
            _gameContext.ReplaceCurrentMergeNumber(e.bubbleNumber.Value);
        }
        _gameContext.ReplaceGameState(GameState.Merge);
    }
}
