using Entitas;
using UnityEngine;

public enum GameState
{
    InitState = 0,
    LoadState = 1,
    FireState = 2,
    FlyState = 3,
    MergeState = 4,
    ScrollState = 5,
}

public class GameController : MonoBehaviour
{
    private Entitas.Systems _systems;
    private Contexts _contexts;
    private bool _initialized = false;
    
    private void Start()
    {
        _contexts = Contexts.sharedInstance;
        _systems = CreateSystems(_contexts);
    }

    private void Update()
    {
        if (!_initialized) return;
        _systems.Execute();
        _systems.Cleanup();
    }

    public void Initialize()
    {
        if (!_initialized)
        {
            _systems.Initialize();
            _initialized = true;
        }
    }

    private static Entitas.Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Systems")
            .Add(new InputSystems(contexts))
            .Add(new GamePlaySystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}