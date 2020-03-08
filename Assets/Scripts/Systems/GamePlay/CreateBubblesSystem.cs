using Entitas;
using UnityEngine;

public class CreateBubblesSystem : IInitializeSystem
{
    private readonly int[] _bubbleNumbers = {2, 4, 8, 16, 32, 64};
    private readonly GameContext _gameContext;
    public CreateBubblesSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }
    public void Initialize()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var bubble = _gameContext.CreateEntity();
                var slotX = j * 2 + i % 2;
                var slotY = i;
                bubble.AddBubbleSlotPos(new Vector2Int(slotX, slotY));
                var bubbleIndex = UnityEngine.Random.Range(0, 6);
                bubble.AddBubbleNumber(_gameContext.GetEntityWithBubbleSetting(_bubbleNumbers[bubbleIndex]).bubbleSetting.Number);
                bubble.isInSlotBubble = true;
            }
        }
    }
}
