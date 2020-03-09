using System;
using Entitas;
using Features;
using UnityEngine;

public enum AudioType
{
    None = 0,
    Transition = 1,
    Bubble = 2,
    Explode2K = 3,
    Perfect = 4,
}

public enum GameState
{
    Init = 0,
    Reload = 1,
    Fire = 2,
    Flying = 3,
    Merge = 4,
    Scroll = 5,
}

public static class BubbleAnimation
{
    public const string Init = "BubbleInitAnim";
    public const string InitSmall = "BubbleInitAsSmallAnim";
    public const string ToLaunch = "BubbleToLunchAnim";
    
    public const string Bump30 = "BubbleBump30Anim";
    public const string Bump90 = "BubbleBump90Anim";
    public const string Bump150 = "BubbleBump150Anim";
    public const string Bump210 = "BubbleBump210Anim";
    public const string Bump270 = "BubbleBump270Anim";
    public const string Bump330 = "BubbleBump330Anim";
}

public class GameController : MonoBehaviour
{
    private Contexts _contexts;
    private bool _initialized = false;

    private Systems _commonSystems;
    private static InitializeFeature _initializeFeature;
    private static ReloadFeature _reloadFeature;
    private static FireFeature _fireFeature;
    private static FlyingFeature _flyingFeature;
    private static MergeFeature _mergeFeature;
    private static ScrollFeature _scrollFeature;
    
    private void Start()
    {
        _contexts = Contexts.sharedInstance;
        
        _commonSystems = CreateSystems(_contexts);
        
        _initializeFeature = new InitializeFeature(_contexts);
        _reloadFeature = new ReloadFeature(_contexts);
        _fireFeature = new FireFeature(_contexts);
        _flyingFeature = new FlyingFeature(_contexts);
        _mergeFeature = new MergeFeature(_contexts);
        _scrollFeature = new ScrollFeature(_contexts);
    }
    
    public void Initialize()
    {
        if (!_initialized)
        {
            _initializeFeature.Initialize();
            _reloadFeature.Initialize();
            _fireFeature.Initialize();
            _flyingFeature.Initialize();
            _mergeFeature.Initialize();
            _scrollFeature.Initialize();
            _commonSystems.Initialize();
            _initialized = true;
        }
    }

    private void Update()
    {
        if (!_initialized) return;
        
        
        switch (_contexts.game.gameState.Value)
        {
            case GameState.Reload:
                _reloadFeature.Execute();
                _commonSystems.Execute();
                _reloadFeature.Cleanup();
                _commonSystems.Cleanup();
                break;
            case GameState.Fire:
                _fireFeature.Execute();
                _commonSystems.Execute();
                _fireFeature.Cleanup();
                _commonSystems.Cleanup();
                break;
            case GameState.Flying:
                _flyingFeature.Execute();
                _commonSystems.Execute();
                _flyingFeature.Cleanup();
                _commonSystems.Cleanup();
                break;
            case GameState.Merge:
                _mergeFeature.Execute();
                _commonSystems.Execute();
                _mergeFeature.Cleanup();
                _commonSystems.Cleanup();
                break;
            case GameState.Scroll:
                _scrollFeature.Execute();
                _commonSystems.Execute();
                _scrollFeature.Cleanup();
                _commonSystems.Cleanup();
                break;
            default:
                _commonSystems.Execute();
                _commonSystems.Cleanup();
                break;
                
        }

    }


    private static Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Systems")
            .Add(new InputSystems(contexts))
            .Add(new GamePlaySystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}