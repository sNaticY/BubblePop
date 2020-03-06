using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class PrepareLaunchSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    

    public PrepareLaunchSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    public void Initialize()
    {
        var curLaunch = CreateNewBubble();
        curLaunch.isWaitingForLaunch = true;
        var nextLaunch = CreateNewBubble();
        nextLaunch.isNextLaunch = true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WaitingForLaunch);

    }

    protected override bool Filter(GameEntity entity)
    {
        return _gameContext.isWaitingForLaunch == false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _gameContext.nextLaunchEntity.isWaitingForLaunch = true;
        _gameContext.isNextLaunch = false;
        var bubble = CreateNewBubble();
        bubble.isNextLaunch = true;
    }

    private GameEntity CreateNewBubble()
    {
        var bubble = _gameContext.CreateEntity();
        bubble.AddPosition(new Vector2(0, 0));
        var bubbleIndex = UnityEngine.Random.Range(0, _gameContext.settings.BubbleTotalCount);
        bubble.AddBubbleIndex(bubbleIndex);
        bubble.AddBubbleNumber(_gameContext.GetEntityWithBubbleSetting(bubbleIndex).bubbleSetting.Number);
        return bubble;
    }

}
