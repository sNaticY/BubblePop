using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class SettingsComponent : IComponent
{
    public GameSettings Value;
}

[Game]
public class BubbleSettingComponent : IComponent
{
    public Color Color;
    public Sprite Sprite;
    [PrimaryEntityIndex] 
    public int Number;
}