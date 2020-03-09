
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class RenderParticleSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    private readonly GameObject _explodePrefab = Resources.Load<GameObject>("BubbleExplodePrefab");
    private readonly Animation _2kAnimation = GameObject.Find("Image_2K").GetComponent<Animation>();
    
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
            var bubbleSettings = _gameContext.GetEntityWithBubbleSetting(entity.bubbleNumber.Value);
            if (bubbleSettings != null)
            {
                var color = bubbleSettings.bubbleSetting.Color;
                mainParticle.startColor = color.linear;
                GameObject.Destroy(particleGameObject, 2f);
            }

            if (entity.bubbleNumber.Value >= 2048)
            {
                _2kAnimation.gameObject.transform.position = position;
                _2kAnimation.Play("2KAnim");
            }
            
        }
    }
}
