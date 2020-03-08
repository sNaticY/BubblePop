
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleCheckMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubbleCheckMergeSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NewBubbleInSlot);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            ProcessMergeableBubble(gameEntity, gameEntity.bubbleNumber.Value, gameEntity.bubbleNumber.Value);
        }


    }

    private int ProcessMergeableBubble(GameEntity entity, int tempNumber, int number)
    {
        List<GameEntity> mergeableEntities = new List<GameEntity>();
        CheckMergeableBubble(entity, tempNumber, number, ref mergeableEntities);
        
        foreach (var e in _gameContext.GetEntities(GameMatcher.MergeableCheck))
        {
            e.isMergeableCheck = false;
        }
        
        var result = number * (int)Mathf.Pow(2, mergeableEntities.Count - 1);
        if (mergeableEntities.Count > 1)
        {
            var maxResult = result;
            GameEntity mergeCore = entity;
            foreach (var mergeable in mergeableEntities)
            {
                var r = ProcessMergeableBubble(mergeable, result, result);
                if (r >= maxResult)
                {
                    maxResult = r;
                    mergeCore = mergeable;
                }
            }

            foreach (var mergeable in mergeableEntities)
            {
                if(mergeable != mergeCore)
                    mergeable.ReplaceReadyToMerge(number, result, mergeCore.bubbleSlotPos.Value);
            }
            result = maxResult;
        }

        return result;
    }

    private void CheckMergeableBubble(GameEntity entity,int tempNumber, int number, ref List<GameEntity> mergeableEntities)
    {
        entity.isMergeableCheck = true;
        if (entity.hasBubbleSlotPos && tempNumber == number)
        {
            mergeableEntities.Add(entity);

            var e30 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.up);
            var e90 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.right);
            var e150 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.right + Vector2Int.down);
            var e210 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.down);
            var e270 = _gameContext.GetEntityWithBubbleSlotPos(entity.bubbleSlotPos.Value + 2 * Vector2Int.left);
            var e330 = _gameContext.GetEntityWithBubbleSlotPos(
                entity.bubbleSlotPos.Value + Vector2Int.left + Vector2Int.up);

            if (e30 != null && !e30.isMergeableCheck)
                CheckMergeableBubble(e30, e30.bubbleNumber.Value, number, ref mergeableEntities);
            if (e90 != null && !e90.isMergeableCheck)
                CheckMergeableBubble(e90, e90.bubbleNumber.Value, number, ref mergeableEntities);
            if (e150 != null && !e150.isMergeableCheck)
                 CheckMergeableBubble(e150, e150.bubbleNumber.Value, number, ref mergeableEntities);
            if (e210 != null && !e210.isMergeableCheck)
                CheckMergeableBubble(e210, e210.bubbleNumber.Value, number, ref mergeableEntities);
            if (e270 != null && !e270.isMergeableCheck)
                 CheckMergeableBubble(e270, e270.bubbleNumber.Value, number, ref mergeableEntities);
            if (e330 != null && !e330.isMergeableCheck)
                 CheckMergeableBubble(e330, e330.bubbleNumber.Value, number, ref mergeableEntities);

        }
    }
}
