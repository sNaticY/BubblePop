
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ShowPerfectSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    private readonly Animation _perfectAnimation = GameObject.Find("Text_Perfect").GetComponent<Animation>();
    
    public ShowPerfectSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.Value == GameState.Scroll;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        for (int i = 0; i < 12; i++)
        {
            var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(i, 0));
            if (entity != null) return;
        }
        _perfectAnimation.Play("ShowPerfectAnim");
    }
}
