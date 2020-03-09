using Entitas;
using UnityEngine;

public class InitSlotBubblesSystem : IInitializeSystem
{
    private readonly GameContext _gameContext;
    public InitSlotBubblesSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }
    public void Initialize()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var bubbleCreation = _gameContext.CreateEntity();
                var slotX = j * 2 + i % 2;
                var slotY = i - 2;
                var targetSlot = new Vector2Int(slotX, slotY);
                var bubbleNumber = GameSettings.GetRandomBubbleNumber(0, 5);
                bubbleCreation.ReplaceBubbleCreation(bubbleNumber, targetSlot);
            }
        }
        _gameContext.ReplaceGameState(GameState.Reload);
    }
}
