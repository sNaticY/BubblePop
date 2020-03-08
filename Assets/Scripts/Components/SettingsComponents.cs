using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class SettingsComponent : IComponent
{
    public float BubbleSize;
    public float BubbleLineSpace;
    public int BubbleTotalCount;
    public float BubbleSpeed;
}

[Game]
public class BubbleSettingComponent : IComponent
{
    public Sprite Sprite;
    [PrimaryEntityIndex] 
    public int Number;
}