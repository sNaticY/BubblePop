
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
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.Value == GameState.Merge && _gameContext.isNewBubble;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var newBubble = _gameContext.newBubbleEntity;
        Dictionary<GameEntity, ReadyToMerge> mergeable = new Dictionary<GameEntity, ReadyToMerge>();
        ProcessMergeableBubble(newBubble, newBubble.bubbleNumber.Value, newBubble.bubbleNumber.Value, mergeable);
        foreach (var merge in mergeable)
        {
            merge.Key.ReplaceComponent(GameComponentsLookup.ReadyToMerge, merge.Value);
        }

        newBubble.isNewBubble = false;
    }

    private int ProcessMergeableBubble(GameEntity entity, int tempNumber, int number, Dictionary<GameEntity, ReadyToMerge> mergeableEntities)
    {
        List<GameEntity> mergeables = new List<GameEntity>();
        CheckMergeableBubble(entity, tempNumber, number, ref mergeables);
        
        foreach (var e in _gameContext.GetEntities(GameMatcher.MergeableCheck))
        {
            e.isMergeableCheck = false;
        }

        var result = number * (int) Mathf.Pow(2, mergeables.Count - 1);
        if (mergeables.Count > 1)
        {
            var maxResult = result;
            GameEntity mergeCore = entity;
            Dictionary<GameEntity, ReadyToMerge> maxSubMergeable = null;
            foreach (var mergeable in mergeables)
            {
                Dictionary<GameEntity, ReadyToMerge> subMergeable = new Dictionary<GameEntity, ReadyToMerge>();
                var r = ProcessMergeableBubble(mergeable, result, result, subMergeable);
                if (r == maxResult)
                {
                    if (mergeable.bubbleSlotPos.Value.y < mergeCore.bubbleSlotPos.Value.y)
                    {
                        maxResult = r;
                        maxSubMergeable = subMergeable;
                        mergeCore = mergeable;
                    }
                }
                else if (r > maxResult)
                {
                    maxResult = r;
                    maxSubMergeable = subMergeable;
                    mergeCore = mergeable;
                }
            }

            foreach (var mergeable in mergeables)
            {
                if(mergeable != mergeCore)
                    mergeableEntities.Add(mergeable, new ReadyToMerge(){Number = tempNumber, TargetNumber = result, TargetSlot = mergeCore.bubbleSlotPos.Value});
            }

            if (maxSubMergeable != null)
            {
                foreach (var mergeable in maxSubMergeable)
                {
                    mergeableEntities.Add(mergeable.Key, mergeable.Value);
                }
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

            if (e30 != null && !e30.isMergeableCheck && e30.isActiveBubble)
                CheckMergeableBubble(e30, e30.bubbleNumber.Value, number, ref mergeableEntities);
            if (e90 != null && !e90.isMergeableCheck && e90.isActiveBubble)
                CheckMergeableBubble(e90, e90.bubbleNumber.Value, number, ref mergeableEntities);
            if (e150 != null && !e150.isMergeableCheck && e150.isActiveBubble)
                 CheckMergeableBubble(e150, e150.bubbleNumber.Value, number, ref mergeableEntities);
            if (e210 != null && !e210.isMergeableCheck && e210.isActiveBubble)
                CheckMergeableBubble(e210, e210.bubbleNumber.Value, number, ref mergeableEntities);
            if (e270 != null && !e270.isMergeableCheck && e270.isActiveBubble)
                 CheckMergeableBubble(e270, e270.bubbleNumber.Value, number, ref mergeableEntities);
            if (e330 != null && !e330.isMergeableCheck && e330.isActiveBubble)
                 CheckMergeableBubble(e330, e330.bubbleNumber.Value, number, ref mergeableEntities);

        }
    }
}
