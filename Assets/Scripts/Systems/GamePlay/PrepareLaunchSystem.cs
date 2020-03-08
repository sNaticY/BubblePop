using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class PrepareLaunchSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    private readonly RectTransform _launchPoint = GameObject.Find("Point_Origin").GetComponent<RectTransform>();
    private readonly RectTransform _nextLaunchPoint = GameObject.Find("Point_Next").GetComponent<RectTransform>();

    public PrepareLaunchSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    public void Initialize()
    {
        var nextLaunch = CreateNewBubble();
        nextLaunch.isNextLaunch = true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _gameContext.isWaitingForLaunch == false && entity.gameState.Value == GameState.Reload;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var bubble = _gameContext.nextLaunchEntity;
        bubble.isWaitingForLaunch = true;
        bubble.isNextLaunch = false;
        bubble.ReplaceBubbleTargetPos(_launchPoint.anchoredPosition);
        bubble.ReplaceSpeed(_gameContext.settings.Value.BubblePrepareLaunchSpeed);
        bubble.ReplaceAnimation(0, BubbleAnimation.ToLaunch);
        
        var newBubble = CreateNewBubble();
        newBubble.isNextLaunch = true;
        newBubble.ReplaceAnimation(0, BubbleAnimation.InitSmall);
    }

    private GameEntity CreateNewBubble()
    {
        var bubble = _gameContext.CreateEntity();
        bubble.AddPosition(_nextLaunchPoint.anchoredPosition);
        bubble.AddBubbleNumber(GameSettings.GetRandomBubbleNumber(0, 5));
        return bubble;
    }

}
