using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;

[Game, Unique]
public class GameStateComponent : IComponent
{
    public GameState Value;
}