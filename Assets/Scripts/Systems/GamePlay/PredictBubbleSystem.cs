using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PredictBubbleSystem : ReactiveSystem<InputEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;
    
    public void Initialize()
    {
        var bubble = _gameContext.CreateEntity();
        bubble.AddBubbleSlotPos(new Vector2Int(-1, -1));
        bubble.AddBubbleNumber(_gameContext.GetEntityWithBubbleSetting(2).bubbleSetting.Number);
        bubble.isPredictBubble = true;
    }
    
    public PredictBubbleSystem(Contexts contexts) : base(contexts.input)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.RayCollision);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasRayCollision;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.rayCollision.SlotPos.x != -1)
            {
                var slotPos = new Vector2Int();
                
                if (Mathf.Abs(entity.rayCollision.Angle) > 100)
                {
                    slotPos.y = entity.rayCollision.SlotPos.y + 1;
                    if (entity.rayCollision.Angle > 0)
                    {
                        slotPos.x = entity.rayCollision.SlotPos.x - 1;
                    }
                    else
                    {
                        slotPos.x = entity.rayCollision.SlotPos.x + 1;
                    }

                }
                else
                {
                    slotPos.y = entity.rayCollision.SlotPos.y;
                    if (entity.rayCollision.Angle > 0)
                    {
                        slotPos.x = entity.rayCollision.SlotPos.x - 2;
                    }
                    else
                    {
                        slotPos.x = entity.rayCollision.SlotPos.x + 2;
                    }
                }
                
                if(_gameContext.GetEntityWithBubbleSlotPos(slotPos) == null)
                    _gameContext.predictBubbleEntity.ReplaceBubbleSlotPos(slotPos);
            }
            else
            {
                _gameContext.predictBubbleEntity.ReplaceBubbleSlotPos(new Vector2Int(-1, -1));
            }
        }
    }

}
