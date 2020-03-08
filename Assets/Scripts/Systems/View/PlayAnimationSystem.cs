using System.Collections.Generic;
using Entitas;

public class PlayAnimationSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private readonly GameContext _gameContext;
    public PlayAnimationSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Animation);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.bubbleView.Value.PlayAnimationDelay(entity.animation.AnimationName, entity.animation.Delay);
        }
    }

    public void Cleanup()
    {
        var entities = _gameContext.GetEntities(GameMatcher.Animation);
        for (var i = entities.Length - 1; i >= 0; i--)
        {
            entities[i].RemoveAnimation();
        }

    }
}
