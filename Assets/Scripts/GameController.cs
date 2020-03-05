using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Entitas.Systems _systems;
    private Contexts _contexts;
    
    

    private void Start()
    {
        _contexts = Contexts.sharedInstance;
        _systems = CreateSystems(_contexts);
        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    private static Entitas.Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Systems")
            .Add(new InputSystems(contexts))
            .Add(new GamePlaySystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}