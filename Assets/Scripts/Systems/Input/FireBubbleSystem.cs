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
        return entity.hasMouseUp && _inputContext.hasRayCollision && _gameContext.isWaitingForLaunch &&
               _gameContext.predictBubbleEntity.hasBubbleSlotPos && _gameContext.gameState.Value == GameState.Fire;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var predictEntity = _gameContext.predictBubbleEntity;
        if (predictEntity.bubbleSlotPos.Value.x != -1)
        {
            var bubbleEntity = _gameContext.waitingForLaunchEntity;
            bubbleEntity.AddBubbleTargetSlot(
                _inputContext.rayCollision.BoundPos -
                (_gameBoard.position - new Vector3(_gameBoard.sizeDelta.x / 2, 0)), predictEntity.position.Value,
                predictEntity.bubbleSlotPos.Value);
            bubbleEntity.ReplaceSpeed(_gameContext.settings.Value.BubbleFlySpeed);
            bubbleEntity.bubbleView.Value.TrailEffect.SetActive(true);
            bubbleEntity.isWaitingForLaunch = false;
            
            _gameContext.CreateEntity().ReplacePlayAudio(AudioType.Transition);
            _gameContext.ReplaceGameState(GameState.Flying);
        }

        _inputContext.ReplaceRayCollision(new Vector2Int(-1, -1), null, Vector2.zero, 0);
        
    }
}