
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProgressionSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ICleanupSystem
{
    private readonly GameContext _gameContext;
    
    public void Initialize()
    {
        var level = 0;
        var score = PlayerPrefs.HasKey("Score") ? PlayerPrefs.GetInt("Score") : 0;
        for (var i = 0; i < _gameContext.settings.Value.Progression.Count; i++)
        {
            var minScore = _gameContext.settings.Value.Progression[i];
            if (score < minScore)
            {
                break;
            }

            level = i + 1;
        }
        _gameContext.ReplaceProgression(level, score);
    }
    
    public ProgressionSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Score);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.score.Value > 0;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var level = _gameContext.progression.Level;
        var score = _gameContext.progression.Score;
        
        foreach (var entity in entities)
        {
            score += entity.score.Value;
        }

        if (level >= _gameContext.settings.Value.Progression.Count - 1)
        {
            var maxLevel = _gameContext.settings.Value.Progression.Count - 1;
            score = Math.Min(score, _gameContext.settings.Value.Progression[maxLevel]);
        }
        else
        {
            while (score > _gameContext.settings.Value.Progression[level])
            {
                level++;
            }
        }

        _gameContext.ReplaceProgression(level, score);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }


    public void Cleanup()
    {
        var entities = _gameContext.GetEntities(GameMatcher.Score);
        for (var index = entities.Length - 1; index >= 0; index--)
        {
            var entity = entities[index];
            entity.Destroy();
        }
    }
}
