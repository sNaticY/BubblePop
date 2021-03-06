//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BubbleCreationComponent bubbleCreation { get { return (BubbleCreationComponent)GetComponent(GameComponentsLookup.BubbleCreation); } }
    public bool hasBubbleCreation { get { return HasComponent(GameComponentsLookup.BubbleCreation); } }

    public void AddBubbleCreation(int newNumber, UnityEngine.Vector2Int newSlot) {
        var index = GameComponentsLookup.BubbleCreation;
        var component = (BubbleCreationComponent)CreateComponent(index, typeof(BubbleCreationComponent));
        component.Number = newNumber;
        component.Slot = newSlot;
        AddComponent(index, component);
    }

    public void ReplaceBubbleCreation(int newNumber, UnityEngine.Vector2Int newSlot) {
        var index = GameComponentsLookup.BubbleCreation;
        var component = (BubbleCreationComponent)CreateComponent(index, typeof(BubbleCreationComponent));
        component.Number = newNumber;
        component.Slot = newSlot;
        ReplaceComponent(index, component);
    }

    public void RemoveBubbleCreation() {
        RemoveComponent(GameComponentsLookup.BubbleCreation);
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

    static Entitas.IMatcher<GameEntity> _matcherBubbleCreation;

    public static Entitas.IMatcher<GameEntity> BubbleCreation {
        get {
            if (_matcherBubbleCreation == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BubbleCreation);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBubbleCreation = matcher;
            }

            return _matcherBubbleCreation;
        }
    }
}
