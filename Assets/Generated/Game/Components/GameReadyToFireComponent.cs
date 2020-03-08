//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ReadyToFire readyToFireComponent = new ReadyToFire();

    public bool isReadyToFire {
        get { return HasComponent(GameComponentsLookup.ReadyToFire); }
        set {
            if (value != isReadyToFire) {
                var index = GameComponentsLookup.ReadyToFire;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : readyToFireComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherReadyToFire;

    public static Entitas.IMatcher<GameEntity> ReadyToFire {
        get {
            if (_matcherReadyToFire == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReadyToFire);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReadyToFire = matcher;
            }

            return _matcherReadyToFire;
        }
    }
}