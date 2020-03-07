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
    [PrimaryEntityIndex] 
    public int Index;

    public Sprite Sprite;
    public int Number;
}