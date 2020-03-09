using System;
using System.Collections.Generic;
using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

public class BubbleScrollSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    
    public BubbleScrollSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.Value == GameState.Scroll;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        for (int i = 7; i >= -1; i--)
        {
            for (int j = 0; j < 12; j++)
            {
                var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(j, i));
                if (entity != null)
                {
                    if (entity.bubbleSlotPos.Value.y >= 7)
                    {
                        ScrollUp();
                    }
                    else if (entity.bubbleSlotPos.Value.y > 0 && entity.bubbleSlotPos.Value.y < 5)
                    {
                        ScrollDown();
                    }
                    else if (entity.bubbleSlotPos.Value.y <= 0)
                    {
                        ScrollDown();
                        _gameContext.ReplaceGameState(GameState.Scroll);
                    }

                    _gameContext.ReplaceGameState(GameState.Reload);
                    return;
                }
            }
        }
    }

    private void ScrollUp()
    {
        DestroyLine(-2);
        for (int i = -1; i < 8; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(j, i));
                if (entity == null) continue;
                entity.ReplaceBubbleSlotPos(entity.bubbleSlotPos.Value - new Vector2Int(0, 1));
                entity.isActiveBubble = entity.bubbleSlotPos.Value.y >= 0;
            }
        }
    }
    
    private void ScrollDown()
    {
        for (int i = 6; i >= -2; i--)
        {
            for (int j = 0; j < 12; j++)
            {
                var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(j, i));
                if (entity == null) continue;
                entity.ReplaceBubbleSlotPos(entity.bubbleSlotPos.Value + new Vector2Int(0, 1));
                entity.isActiveBubble = entity.bubbleSlotPos.Value.y >= 0;
            }
        }
        if(_gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(0, -1)) != null)
        {
            GenerateLine(-2, 1);
        }
        else
        {
            GenerateLine(-2, 0);
        }
    }

    private void GenerateLine(int line, int fromIndex)
    {
        for (int i = fromIndex; i < 12; i += 2)
        {
            var number = GameSettings.GetRandomBubbleNumber(0, 5);
            _gameContext.CreateEntity().AddBubbleCreation(number, new Vector2Int(i, line));
        }
    }

    private void DestroyLine(int line)
    {
        for (int i = 0; i < 12; i++)
        {
            var entity = _gameContext.GetEntityWithBubbleSlotPos(new Vector2Int(i, line));
            if (entity != null)
            {
                entity.bubbleView.Value.gameObject.DestroyGameObject();
                entity.isReadyToDestroy = true;
            }
        }
    }
}
