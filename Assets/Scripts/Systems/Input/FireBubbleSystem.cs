using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FireBubbleSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext _gameContext;
    private readonly InputContext _inputContext;
    
    private readonly RectTransform _gameBoard = GameObject.Find("Panel_GameBoard").GetComponent<RectTransform>();
    
    public FireBubbleSystem(Contexts contexts) : base(contexts.input)
    {
        _gameContext = contexts.game;
        _inputContext = contexts.input;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MouseUp));
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseUp && _inputContext.hasRayCollision && _gameContext.isWaitingForLaunch;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var predictEntity = _gameContext.predictBubbleEntity;
        _gameContext.waitingForLaunchEntity.AddBubbleTargetSlot(_inputContext.rayCollision.BoundPos - (_gameBoard.position - new Vector3(_gameBoard.sizeDelta.x / 2, 0)), predictEntity.position.Value, predictEntity.bubbleSlotPos.Value);
        _inputContext.ReplaceRayCollision(new Vector2Int(-1, -1), null, Vector2.zero, 0);
        _gameContext.waitingForLaunchEntity.isWaitingForLaunch = false;
        _gameContext.isReload = true;
    }
}