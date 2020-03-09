using System.Collections.Generic;
using Entitas;
using Entitas.VisualDebugging.Unity;

public class DestroyBubbleSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private readonly GameContext _gameContext;
    public DestroyBubbleSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReadyToDestroy);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isReadyToDestroy && entity.hasBubbleNumber;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            _gameContext.CreateEntity().ReplaceScore(entity.bubbleNumber.Value);
        }
    }


    public void Cleanup()
    {
        var destroyEntities = _gameContext.GetEntities(GameMatcher.ReadyToDestroy);
        for (int i = destroyEntities.Length - 1; i >= 0; i--)
        {
            destroyEntities[i].bubbleView.Value.gameObject.DestroyGameObject();
            destroyEntities[i].Destroy();
        }
    }
}
