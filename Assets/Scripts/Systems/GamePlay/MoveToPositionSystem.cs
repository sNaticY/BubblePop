using Entitas;
using UnityEngine;

public class MoveToPositionSystem : IExecuteSystem, ICleanupSystem
{
    private readonly IGroup<GameEntity> _movableBubbles;
    private readonly IGroup<GameEntity> _completeMoveBubbles;
    private readonly GameContext _gameContext;

    public MoveToPositionSystem(Contexts contexts)
    {
        _movableBubbles = contexts.game.GetGroup(GameMatcher.AnyOf(GameMatcher.BubbleTargetPos, GameMatcher.BubbleSecondTargetPos));
        _completeMoveBubbles = contexts.game.GetGroup(GameMatcher.CompleteMove);
        _gameContext = contexts.game;
    }
    
    public void Execute()
    {
        foreach (GameEntity e in _movableBubbles.GetEntities())
        {
            if (e.hasBubbleTargetPos && e.hasPosition)
            {
                Vector2 dir = e.bubbleTargetPos.Value - e.position.Value;
                Vector2 newPosition = e.position.Value +
                                      dir.normalized * (e.speed.Value * Time.deltaTime);
                e.ReplacePosition(newPosition);

                var distance = dir.magnitude;
                if (distance <= 200f)
                {
                    e.RemoveBubbleTargetPos();
                    if (!e.hasBubbleSecondTargetPos)
                    {
                        e.isCompleteMove = true;
                    }
                }
            }
            else if(e.hasBubbleSecondTargetPos && e.hasPosition)
            {
                var dir = e.bubbleSecondTargetPos.Value - e.position.Value;
                Vector2 newPosition = e.position.Value +
                                      dir.normalized * (e.speed.Value * Time.deltaTime);
                e.ReplacePosition(newPosition);

                var distance = dir.magnitude;
                if (distance <= 200f)
                {
                    e.RemoveBubbleSecondTargetPos();
                    e.isCompleteMove = true;
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (var e in _completeMoveBubbles)
        {
            if(e.hasBubbleTargetPos)
                e.RemoveBubbleTargetPos();
        }
    }
}