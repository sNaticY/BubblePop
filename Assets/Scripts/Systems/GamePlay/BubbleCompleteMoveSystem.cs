
using System;
using System.Collections.Generic;
using Entitas;

public class BubbleCompleteMoveSystem : ReactiveSystem<GameEntity>
{
    public BubbleCompleteMoveSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CompleteMove);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCompleteMove && entity.hasBubbleTargetSlot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isCompleteMove = false;
            e.AddBubbleSlotPos(e.bubbleTargetSlot.TargetSlot);
            e.isInSlotBubble = true;
            e.bubbleView.Collider.enabled = e.isInSlotBubble;
            e.RemoveBubbleTargetSlot();
        }
    }
}
