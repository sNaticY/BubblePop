using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;

[Game, Unique]
public class GameStateComponent : IComponent
{
    public GameState Value;
}

[Game, Unique]
public class AudioSourceComponent : IComponent
{
    public AudioSource Value;
}

[Game]
public class PlayAudioComponent : IComponent
{
    public AudioType Value;
}

[Game, Unique]
public class ProgressionComponent : IComponent
{
    public int Level;
    public int Score;
}

[Game]
public class ScoreComponent : IComponent
{
    public int Value;
}

[Game, Unique]
public class ProgressionViewComponent : IComponent
{
    public ProgressionController Value;
}