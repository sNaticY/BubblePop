
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class RenderParticleSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    private readonly GameObject _explodePrefab = Resources.Load<GameObject>("BubbleExplodePrefab");
    
    public RenderParticleSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReadyToDestroy);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isReadyToDestroy && entity.hasBubbleView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var position = entity.bubbleView.Value.transform.position;
            var particleGameObject = GameObject.Instantiate(_explodePrefab, position, Quaternion.identity, entity.bubbleView.Value.transform.parent);
            var particleSystem = particleGameObject.GetComponent<ParticleSystem>();
            var mainParticle = particleSystem.main;
            var color = _gameContext.GetEntityWithBubbleSetting(entity.bubbleNumber.Value).bubbleSetting.Color;
            mainParticle.startColor = color.linear;
            GameObject.Destroy(particleGameObject, 2f);
            
        }
    }
}
