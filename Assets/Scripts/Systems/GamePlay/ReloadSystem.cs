using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class ReloadSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    private readonly RectTransform _launchPoint = GameObject.Find("Point_Origin").GetComponent<RectTransform>();
    private readonly RectTransform _nextLaunchPoint = GameObject.Find("Point_Next").GetComponent<RectTransform>();

    public ReloadSystem(Contexts contexts) : base(contexts.game)
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
        return  entity.gameState.Value == GameState.Reload;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_gameContext.isWaitingForLaunch == false)
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

            _gameContext.predictBubbleEntity.ReplaceBubbleNumber(bubble.bubbleNumber.Value);
        }
        else
        {
            _gameContext.ReplaceGameState(GameState.Fire);
        }
    }

    private GameEntity CreateNewBubble()
    {
        var bubble = _gameContext.CreateEntity();
        bubble.AddPosition(_nextLaunchPoint.anchoredPosition);
        bubble.AddBubbleNumber(GameSettings.GetRandomBubbleNumber(0, 5));
        return bubble;
    }

}
