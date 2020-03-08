using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BubbleMoveToSlotSystem : ReactiveSystem<GameEntity>
{
    public BubbleMoveToSlotSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.BubbleTargetSlot));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            if (e.bubbleTargetSlot.BoundPos != null)
            {
                e.ReplaceBubbleTargetPos(e.bubbleTargetSlot.BoundPos.Value);
                e.ReplaceBubbleSecondTargetPos(e.bubbleTargetSlot.TargetPos);
            }
            else
            {
                e.ReplaceBubbleTargetPos(e.bubbleTargetSlot.TargetPos);
            }
        }
    }
}