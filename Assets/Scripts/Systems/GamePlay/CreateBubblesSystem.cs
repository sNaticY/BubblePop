using Entitas;
using UnityEngine;

public class CreateBubblesSystem : IInitializeSystem
{
    private readonly GameContext _gameContext;
    public CreateBubblesSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }
    public void Initialize()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var bubble = _gameContext.CreateEntity();
                var slotX = j * 2 + i % 2;
                var slotY = i;
                bubble.AddBubbleSlotPos(new Vector2Int(slotX, slotY));
                var bubbleIndex = UnityEngine.Random.Range(0, _gameContext.settings.BubbleTotalCount);
                bubble.AddBubbleIndex(bubbleIndex);
                bubble.AddBubbleNumber(_gameContext.GetEntityWithBubbleSetting(bubbleIndex).bubbleSetting.Number);
                bubble.isInSlotBubble = true;
            }
        }
    }
}
