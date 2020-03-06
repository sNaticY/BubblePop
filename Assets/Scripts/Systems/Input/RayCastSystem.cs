using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class RayCastSystem : ReactiveSystem<InputEntity>
{
    private readonly InputContext _inputContext;
    private readonly GameObject _originPoint = GameObject.Find("Point_Origin");
    
    public RayCastSystem(Contexts contexts) : base(contexts.input)
    {
        _inputContext = contexts.input;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MousePosition));
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMousePosition;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var entity in entities)
        {
            var mousePos = entity.mousePosition.Position;
            var originPos = (Vector2) _originPoint.transform.position;
            var direction = (mousePos - originPos).normalized;
            var hit = Physics2D.Raycast(originPos, direction);
            Debug.DrawLine(originPos, hit.point, Color.yellow);
            
            if (hit.transform == null)
            {
                Debug.LogWarning("Hit Transform == null");
                continue;
            };
            
            if(hit.transform.CompareTag("Ball"))
            {
                var e = (GameEntity) hit.transform.gameObject.GetEntityLink().entity;
                var angle = Vector2.SignedAngle(Vector2.up, hit.point - (Vector2) hit.transform.position);
                _inputContext.ReplaceRayCollision(e.bubbleSlotPos.Value, null, hit.point, angle);
                return;
            }
            
            if (hit.transform.CompareTag("Bound"))
            {
                var secondDirection = new Vector2(-direction.x, direction.y);
                
                // Tolerant to prevent origin point inside collider
                var secondHit = Physics2D.Raycast(hit.point + new Vector2(-direction.x, 0), secondDirection);
                
                Debug.DrawLine(hit.point, secondHit.point, Color.red);

                if (secondHit.transform != null && secondHit.transform.CompareTag("Ball"))
                {
                    var e = (GameEntity) secondHit.transform.gameObject.GetEntityLink().entity;
                    var angle = Vector2.SignedAngle(Vector2.up, secondHit.point - (Vector2) secondHit.transform.position);
                    _inputContext.ReplaceRayCollision(e.bubbleSlotPos.Value, hit.point, secondHit.point, angle);
                    return;
                }
                _inputContext.ReplaceRayCollision(new Vector2Int(-1, -1), null, Vector2.zero, 0);
                
                
                
            }
        }
    }
}
