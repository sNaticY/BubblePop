
using System.Collections.Generic;
using Entitas;

public class PredictBubbleSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext _gameContext;
    
    public PredictBubbleSystem(IContext<InputEntity> context) : base(context)
    {
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.RayCollision);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasRayCollision && entity.rayCollision.SlotPos.x != -1;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        
    }
}
