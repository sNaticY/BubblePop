using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderPositionSystem : ReactiveSystem<GameEntity>
{
    private Transform _container = GameObject.Find("Point_Origin").transform;

    public RenderPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasBubbleView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            if(e.isWaitingForLaunch)
                e.bubbleView.GameObject.transform.position = _container.transform.position;
            else
            {
                e.bubbleView.RectTransform.anchoredPosition = e.position.value;
            }
        }
    }
}