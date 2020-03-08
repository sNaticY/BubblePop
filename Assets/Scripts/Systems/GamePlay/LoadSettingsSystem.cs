using Entitas;
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
        _gameContext.ReplaceSettings(gameSettings);
        for (var i = 0; i < gameSettings.BubbleSettings.Count; i++)
        {
            var settings = gameSettings.BubbleSettings[i];
            var settingEntity = _gameContext.CreateEntity();
            settingEntity.ReplaceBubbleSetting(settings.Sprite, settings.Number);
        }
    }
}
