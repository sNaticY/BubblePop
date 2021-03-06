//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity audioSourceEntity { get { return GetGroup(GameMatcher.AudioSource).GetSingleEntity(); } }
    public AudioSourceComponent audioSource { get { return audioSourceEntity.audioSource; } }
    public bool hasAudioSource { get { return audioSourceEntity != null; } }

    public GameEntity SetAudioSource(UnityEngine.AudioSource newValue) {
        if (hasAudioSource) {
            throw new Entitas.EntitasException("Could not set AudioSource!\n" + this + " already has an entity with AudioSourceComponent!",
                "You should check if the context already has a audioSourceEntity before setting it or use context.ReplaceAudioSource().");
        }
        var entity = CreateEntity();
        entity.AddAudioSource(newValue);
        return entity;
    }

    public void ReplaceAudioSource(UnityEngine.AudioSource newValue) {
        var entity = audioSourceEntity;
        if (entity == null) {
            entity = SetAudioSource(newValue);
        } else {
            entity.ReplaceAudioSource(newValue);
        }
    }

    public void RemoveAudioSource() {
        audioSourceEntity.Destroy();
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
public partial class GameEntity {

    public AudioSourceComponent audioSource { get { return (AudioSourceComponent)GetComponent(GameComponentsLookup.AudioSource); } }
    public bool hasAudioSource { get { return HasComponent(GameComponentsLookup.AudioSource); } }

    public void AddAudioSource(UnityEngine.AudioSource newValue) {
        var index = GameComponentsLookup.AudioSource;
        var component = (AudioSourceComponent)CreateComponent(index, typeof(AudioSourceComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAudioSource(UnityEngine.AudioSource newValue) {
        var index = GameComponentsLookup.AudioSource;
        var component = (AudioSourceComponent)CreateComponent(index, typeof(AudioSourceComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAudioSource() {
        RemoveComponent(GameComponentsLookup.AudioSource);
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

    static Entitas.IMatcher<GameEntity> _matcherAudioSource;

    public static Entitas.IMatcher<GameEntity> AudioSource {
        get {
            if (_matcherAudioSource == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AudioSource);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAudioSource = matcher;
            }

            return _matcherAudioSource;
        }
    }
}
