using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    private readonly Transform _viewContainer = GameObject.Find("Panel_GameBoard").transform;
    private readonly GameContext _context;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BubbleIndex);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBubbleIndex && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            var prefab = Resources.Load<GameObject>("BubblePrefab");
            GameObject go = Object.Instantiate(prefab, _viewContainer);
            var view = go.GetComponent<BubbleView>();
            e.AddView(go, view.RectTransform, view.Image, view.Text, view.Collider);
            go.Link(e);
        }
    }
}