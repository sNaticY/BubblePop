
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckMergeSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public CheckMergeSystem(Contexts contexts) : base(contexts.game)
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
        
        Dictionary<GameEntity, ReadyToMerge> readyToMerges = new Dictionary<GameEntity, ReadyToMerge>();
        CalcMergeValue(newBubble, newBubble.bubbleNumber.Value, newBubble.bubbleNumber.Value, readyToMerges);
        
        foreach (var merge in readyToMerges)
        {
            if(merge.Key.isInSlotBubble)
                merge.Key.ReplaceComponent(GameComponentsLookup.ReadyToMerge, merge.Value);
        }

        newBubble.isNewBubble = false;
    }

    private int CalcMergeValue(GameEntity entity, int fakeSelfNumber, int numberToMerge, Dictionary<GameEntity, ReadyToMerge> readyToMerges)
    {
        // Find all connected same value bubbles
        List<GameEntity> sameBubbles = new List<GameEntity>();
        FindMergeableBubble(entity, fakeSelfNumber, numberToMerge, ref sameBubbles);
        
        foreach (var e in _gameContext.GetEntities(GameMatcher.MergeableCheck))
        {
            e.isMergeableCheck = false;
        }
        
        var resultNumber = Math.Min(numberToMerge * (uint) Mathf.Pow(2, sameBubbles.Count - 1), 2048);
        
        
        // If more than 1 bubble to merge
        // Calculate merging to which bubble generates bigger number
        if (sameBubbles.Count > 1)
        {
            var maxResultNumber = resultNumber;
            GameEntity mergeCore = entity;
            Dictionary<GameEntity, ReadyToMerge> maxSubReadyToMerges = null;
            
            // For each bubble, check the value of sub merging, take biggest one
            foreach (var bubble in sameBubbles)
            {
                Dictionary<GameEntity, ReadyToMerge> subReadyToMerges = new Dictionary<GameEntity, ReadyToMerge>();
                var r = CalcMergeValue(bubble, (int)resultNumber, (int)resultNumber, subReadyToMerges);
                
                // take the higher one as merge core if result equals
                if (r == maxResultNumber)
                {
                    if (bubble.bubbleSlotPos.Value.y < mergeCore.bubbleSlotPos.Value.y)
                    {
                        maxResultNumber = r;
                        maxSubReadyToMerges = subReadyToMerges;
                        mergeCore = bubble;
                    }
                }
                else if (r > maxResultNumber)
                {
                    maxResultNumber = r;
                    maxSubReadyToMerges = subReadyToMerges;
                    mergeCore = bubble;
                }
            }
            
            // Add most valuable merge approaches into readyToMerges
            foreach (var bubble in sameBubbles)
            {
                if(bubble != mergeCore)
                    readyToMerges.Add(bubble, new ReadyToMerge(){Number = fakeSelfNumber, TargetNumber = (int)resultNumber, TargetSlot = mergeCore.bubbleSlotPos.Value});
            }
            
            // Add most valuable sub merges
            if (maxSubReadyToMerges != null)
            {
                foreach (var mergeable in maxSubReadyToMerges)
                {
                    readyToMerges.Add(mergeable.Key, mergeable.Value);
                }
            }

            resultNumber = Math.Min(maxResultNumber, 2048);
        }

        return (int)resultNumber;
    }

    private void FindMergeableBubble(GameEntity entity,int tempNumber, int number, ref List<GameEntity> mergeableEntities)
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
                FindMergeableBubble(e30, e30.bubbleNumber.Value, number, ref mergeableEntities);
            if (e90 != null && !e90.isMergeableCheck && e90.isActiveBubble)
                FindMergeableBubble(e90, e90.bubbleNumber.Value, number, ref mergeableEntities);
            if (e150 != null && !e150.isMergeableCheck && e150.isActiveBubble)
                 FindMergeableBubble(e150, e150.bubbleNumber.Value, number, ref mergeableEntities);
            if (e210 != null && !e210.isMergeableCheck && e210.isActiveBubble)
                FindMergeableBubble(e210, e210.bubbleNumber.Value, number, ref mergeableEntities);
            if (e270 != null && !e270.isMergeableCheck && e270.isActiveBubble)
                 FindMergeableBubble(e270, e270.bubbleNumber.Value, number, ref mergeableEntities);
            if (e330 != null && !e330.isMergeableCheck && e330.isActiveBubble)
                 FindMergeableBubble(e330, e330.bubbleNumber.Value, number, ref mergeableEntities);

        }
    }
}
