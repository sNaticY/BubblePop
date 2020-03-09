
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class RenderLineViewSystem : ReactiveSystem<InputEntity>, IInitializeSystem
{
    private readonly GameContext _gameContext;

    private readonly Vector2 _originPos =
        GameObject.Find("Point_Origin").GetComponent<RectTransform>().anchoredPosition;

    private readonly Vector2 _canvasScale = GameObject.Find("Canvas_Game").GetComponent<RectTransform>().localScale;

    public RenderLineViewSystem(Contexts contexts) : base(contexts.input)
    {
        _gameContext = contexts.game;
    }

    public void Initialize()
    {
        var line1 = GameObject.Find("Line_1").GetComponentInChildren<UILineRenderer>();
        line1.Points = new Vector2[] {_originPos, _originPos};

        var line2 = GameObject.Find("Line_2").GetComponentInChildren<UILineRenderer>();
        line2.Points = new Vector2[] {Vector2.zero, Vector2.zero};

        _gameContext.SetLineView(line1, line2);
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
            if (entity.rayCollision.SlotPos.x >= 0)
            {
                if (entity.rayCollision.BoundPos == null)
                {
                    _gameContext.lineView.Line1.Points[1] =
                        (entity.rayCollision.CollisionPos -
                         (Vector2) _gameContext.lineView.Line1.gameObject.transform.position) / _canvasScale;
                    _gameContext.lineView.Line1.SetAllDirty();
                    _gameContext.lineView.Line2.Points[0] = Vector2.zero;
                    _gameContext.lineView.Line2.Points[1] = Vector2.zero;
                    _gameContext.lineView.Line2.SetAllDirty();
                }
                else
                {
                    _gameContext.lineView.Line1.Points[1] =
                        (entity.rayCollision.BoundPos.Value -
                         (Vector2) _gameContext.lineView.Line1.gameObject.transform.position) / _canvasScale;
                    _gameContext.lineView.Line1.SetAllDirty();
                    _gameContext.lineView.Line2.Points[0] =
                        (entity.rayCollision.BoundPos.Value -
                         (Vector2) _gameContext.lineView.Line2.gameObject.transform.position) / _canvasScale;
                    _gameContext.lineView.Line2.Points[1] =
                        (entity.rayCollision.CollisionPos -
                         (Vector2) _gameContext.lineView.Line2.gameObject.transform.position) / _canvasScale;
                    _gameContext.lineView.Line2.SetAllDirty();
                }
            }
            else
            {
                _gameContext.lineView.Line1.Points[1] = _originPos;
                _gameContext.lineView.Line1.SetAllDirty();
                _gameContext.lineView.Line2.Points[0] = Vector2.zero;
                _gameContext.lineView.Line2.Points[1] = Vector2.zero;
                _gameContext.lineView.Line2.SetAllDirty();
            }
        }
    }

}
