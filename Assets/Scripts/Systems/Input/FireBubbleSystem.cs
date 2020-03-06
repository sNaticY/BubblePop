using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FireBubbleSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext _gameContext;
    private readonly InputContext _inputContext;
    
    public FireBubbleSystem(Contexts contexts) : base(contexts.input)
    {
        _gameContext = contexts.game;
        _inputContext = contexts.input;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MouseUp));
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseUp && _inputContext.hasRayCollision && _gameContext.isWaitingForLaunch;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        
    }
}