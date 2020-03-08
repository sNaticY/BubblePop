using Entitas;
using Entitas.VisualDebugging.Unity;

public class DestroyBubbleSystem : ICleanupSystem
{
    private readonly GameContext _gameContext;
    public DestroyBubbleSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }
    public void Cleanup()
    {
        var destroyEntities = _gameContext.GetEntities(GameMatcher.ReadyToDestroy);
        for (int i = destroyEntities.Length - 1; i >= 0; i--)
        {
            destroyEntities[i].bubbleView.Value.gameObject.DestroyGameObject();
            destroyEntities[i].Destroy();
        }
    }
}
