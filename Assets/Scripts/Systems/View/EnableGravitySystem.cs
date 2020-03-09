using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EnableGravitySystem : ReactiveSystem<GameEntity>
{
    private GameContext _gameContext;
    
    public EnableGravitySystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReadyToDropDown);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isReadyToDropDown;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        for (var i = entities.Count - 1; i >= 0; i--)
        {
            var entity = entities[i];
            entity.bubbleView.Value.Collider.isTrigger = true;
            entity.isReadyToDropDown = false;
            var rigidBody = entity.bubbleView.Value.gameObject.AddComponent<Rigidbody2D>();
            rigidBody.gravityScale = 200;
            rigidBody.AddForce(new Vector2(Random.Range(-1f, 1f) * 14000, -100));
        }
    }
}
