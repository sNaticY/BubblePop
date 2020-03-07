//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity vibrationOnEntity { get { return GetGroup(InputMatcher.VibrationOn).GetSingleEntity(); } }

    public bool isVibrationOn {
        get { return vibrationOnEntity != null; }
        set {
            var entity = vibrationOnEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isVibrationOn = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly VibrationOnComponent vibrationOnComponent = new VibrationOnComponent();

    public bool isVibrationOn {
        get { return HasComponent(InputComponentsLookup.VibrationOn); }
        set {
            if (value != isVibrationOn) {
                var index = InputComponentsLookup.VibrationOn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : vibrationOnComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherVibrationOn;

    public static Entitas.IMatcher<InputEntity> VibrationOn {
        get {
            if (_matcherVibrationOn == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.VibrationOn);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherVibrationOn = matcher;
            }

            return _matcherVibrationOn;
        }
    }
}