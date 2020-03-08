
using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

public class BubbleCompleteMoveSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private readonly GameContext _gameContext;
    public BubbleCompleteMoveSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CompleteMove);
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
            
            if (e.hasBubbleTargetSlot)
            {
                var predictBubble = _gameContext.predictBubbleEntity;
                if (predictBubble.bubbleSlotPos.Value == e.bubbleTargetSlot.TargetSlot)
                {
                    predictBubble.RemoveBubbleSlotPos();
                }
                e.AddBubbleSlotPos(e.bubbleTargetSlot.TargetSlot);
                e.isNewBubbleInSlot = true;
                e.isInSlotBubble = true;
                e.RemoveBubbleTargetSlot();
                
                _gameContext.ReplaceCurrentMergeNumber(e.bubbleNumber.Value);
            }
            else if (e.isWaitingForLaunch)
            {
                e.isReadyToFire = true;
            }
            else if (e.hasReadyToMerge)
            {
                var targetEntity = _gameContext.GetEntityWithBubbleSlotPos(e.readyToMerge.TargetSlot);
                targetEntity.ReplaceBubbleNumber(e.readyToMerge.TargetNumber);
                if (targetEntity.hasReadyToMerge)
                {
                    _gameContext.ReplaceCurrentMergeNumber(targetEntity.bubbleNumber.Value);
                }
                e.isReadyToDestroy = true;
            }
            
        }
    }

    public void Cleanup()
    {
        var destroyEntities = _gameContext.GetEntities(GameMatcher.ReadyToDestroy);
        for (int i = destroyEntities.Length - 1; i >= 0; i--)
        {
            destroyEntities[i].bubbleView.GameObject.Unlink();
            destroyEntities[i].bubbleView.GameObject.DestroyGameObject();
            destroyEntities[i].Destroy();
        }
    }
}
