using Entitas;
using Settings;
using UnityEngine;

public class LoadSettingsSystem : IInitializeSystem
{
    private readonly GameContext _gameContext;
    
    public LoadSettingsSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }
    
    public void Initialize()
    {
        var gameSettings = Resources.Load<GameSettings>("GameSettings");
        _gameContext.ReplaceSettings(gameSettings.BubbleSize, gameSettings.BubbleLineSpace,
            gameSettings.BubbleSettings.Count);
        for (var i = 0; i < gameSettings.BubbleSettings.Count; i++)
        {
            var settings = gameSettings.BubbleSettings[i];
            var settingEntity = _gameContext.CreateEntity();
            settingEntity.ReplaceBubbleSetting(i, settings.Color, settings.Number);
        }
    }
}
