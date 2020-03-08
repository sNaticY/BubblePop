using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class PrepareLaunchSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    private readonly RectTransform _launchPoint = GameObject.Find("Point_Origin").GetComponent<RectTransform>();
    private readonly RectTransform _nextLaunchPoint = GameObject.Find("Point_Next").GetComponent<RectTransform>();
    private readonly int[] _bubbleNumbers = {2, 4, 8, 16, 32, 64};

    public PrepareLaunchSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    public void Initialize()
    {
        var curLaunch = CreateNewBubble(_launchPoint.anchoredPosition);
        curLaunch.isWaitingForLaunch = true;
        curLaunch.isReadyToFire = true;
        var nextLaunch = CreateNewBubble(_nextLaunchPoint.anchoredPosition);
        nextLaunch.isNextLaunch = true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Reload);

    }

    protected override bool Filter(GameEntity entity)
    {
        return _gameContext.isWaitingForLaunch == false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _gameContext.nextLaunchEntity.isWaitingForLaunch = true;
        _gameContext.nextLaunchEntity.isNextLaunch = false;
        _gameContext.waitingForLaunchEntity.ReplaceBubbleTargetPos(_launchPoint.anchoredPosition);
        var bubble = CreateNewBubble(_nextLaunchPoint.anchoredPosition);
        bubble.isNextLaunch = true;
        _gameContext.isReload = false;
    }

    private GameEntity CreateNewBubble(Vector2 position)
    {
        var bubble = _gameContext.CreateEntity();
        bubble.AddPosition(position);
        var bubbleIndex = UnityEngine.Random.Range(0, 6);
        bubble.AddBubbleNumber(_gameContext.GetEntityWithBubbleSetting(_bubbleNumbers[bubbleIndex]).bubbleSetting.Number);
        return bubble;
    }

}
