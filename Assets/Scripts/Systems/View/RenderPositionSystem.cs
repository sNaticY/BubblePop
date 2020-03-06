using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderPositionSystem : ReactiveSystem<GameEntity>
{
    private readonly Transform _launchPoint = GameObject.Find("Point_Origin").transform;
    private readonly Transform _nextLaunchPoint = GameObject.Find("Point_Next").transform;

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
            if (e.isWaitingForLaunch)
            {
                e.bubbleView.GameObject.transform.position = _launchPoint.transform.position;
            }
            else if (e.isNextLaunch)
            {
                e.bubbleView.GameObject.transform.position = _nextLaunchPoint.transform.position;
            }
            else
            {
                e.bubbleView.RectTransform.anchoredPosition = e.position.value;
            }
        }
    }
}