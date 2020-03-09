
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class ShowProgressionViewSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    
    public ShowProgressionViewSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    public void Initialize()
    {
        _gameContext.ReplaceProgressionView(GameObject.Find("Progression").GetComponent<ProgressionController>());
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Progression);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var bubbleSettings =  _gameContext.settings.Value.BubbleSettings;
        var view = _gameContext.progressionView.Value;
        var progression = _gameContext.progression;
        SetImageAndTextByLevel(ref view.CurLevelImage, ref view.CurLevelText, progression.Level);
        SetImageAndTextByLevel(ref view.NextLevelImage, ref view.NextLevelText, progression.Level + 1);
        view.ProgressBar.value = GetProgressPercentage(progression.Level, progression.Score);
        view.ProgressFill.color = bubbleSettings[progression.Level % bubbleSettings.Count].Color;
        view.CurScore.text = progression.Score.ToString();

    }

    private void SetImageAndTextByLevel(ref Image image, ref Text text, int level)
    {
        var bubbleSettings = _gameContext.settings.Value.BubbleSettings;
        var color = bubbleSettings[level % bubbleSettings.Count].Color;
        image.color = color;
        text.text = level.ToString();
    }

    private float GetProgressPercentage(int level, int score)
    {
        var progressionSettings = _gameContext.settings.Value.Progression;
        var lastLevelScore = (level == 0 ? 0 : progressionSettings[level - 1]);
        var totalAmount = progressionSettings[level] - lastLevelScore;
        var fillAmount = score - lastLevelScore;
        return fillAmount / (float)totalAmount;
    }

}
