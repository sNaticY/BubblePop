using System;
using System.Collections.Generic;
using Entitas;

public class CreateBubbleSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private readonly GameContext _gameContext;
    
    public CreateBubbleSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleCreation);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var bubble = _gameContext.CreateEntity();
            bubble.AddBubbleSlotPos(entity.bubbleCreation.Slot);
            bubble.AddBubbleNumber(entity.bubbleCreation.Number);
            bubble.isInSlotBubble = true;
            bubble.AddAnimation(UnityEngine.Random.Range(0f, 0.4f), BubbleAnimation.Init);
            bubble.isActiveBubble = bubble.bubbleSlotPos.Value.y >= 0;
        }
    }

    public void Cleanup()
    {
        var entities = _gameContext.GetEntities(GameMatcher.BubbleCreation);
        for (var i = entities.Length - 1; i >= 0; i--)
        {
            var entity = entities[i];
            entity.Destroy();
        }
    }
}
