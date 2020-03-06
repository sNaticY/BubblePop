//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly InSlotBubbleComponent inSlotBubbleComponent = new InSlotBubbleComponent();

    public bool isInSlotBubble {
        get { return HasComponent(GameComponentsLookup.InSlotBubble); }
        set {
            if (value != isInSlotBubble) {
                var index = GameComponentsLookup.InSlotBubble;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : inSlotBubbleComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherInSlotBubble;

    public static Entitas.IMatcher<GameEntity> InSlotBubble {
        get {
            if (_matcherInSlotBubble == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InSlotBubble);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInSlotBubble = matcher;
            }

            return _matcherInSlotBubble;
        }
    }
}
