
using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

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
            PlayBumpAnimation(e);
        }
        _gameContext.ReplaceGameState(GameState.Merge);
    }

    private void PlayBumpAnimation(GameEntity entity)
    {
        var e30 = _gameContext.GetEntityWithBubbleSlotPos(
            entity.bubbleSlotPos.Value + Vector2Int.right - Vector2Int.up);
        var e90 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.right);
        var e150 = _gameContext.GetEntityWithBubbleSlotPos(
            entity.bubbleSlotPos.Value + Vector2Int.right - Vector2Int.down);
        var e210 = _gameContext.GetEntityWithBubbleSlotPos(
            entity.bubbleSlotPos.Value + Vector2Int.left - Vector2Int.down);
        var e270 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.left);
        var e330 = _gameContext.GetEntityWithBubbleSlotPos(
            entity.bubbleSlotPos.Value + Vector2Int.left - Vector2Int.up);

        if (e30 != null) e30.ReplaceAnimation(0f, "BubbleBump30Anim");
        if (e90 != null) e90.ReplaceAnimation(0f, "BubbleBump90Anim");
        if (e150 != null) e150.ReplaceAnimation(0f, "BubbleBump150Anim");
        if (e210 != null) e210.ReplaceAnimation(0f, "BubbleBump210Anim");
        if (e270 != null) e270.ReplaceAnimation(0f, "BubbleBump270Anim");
        if (e330 != null) e330.ReplaceAnimation(0f, "BubbleBump330Anim");
    }
}
