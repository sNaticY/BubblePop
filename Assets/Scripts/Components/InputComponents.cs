using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public class MoverComponent : IComponent
{
}

[Input, Unique]
public class LeftMouseComponent : IComponent
{
}

[Input]
public class MouseDownComponent : IComponent
{
    public Vector2 Position;
}

[Input]
public class MousePositionComponent : IComponent
{
    public Vector2 Position;
}

[Input]
public class MouseUpComponent : IComponent
{
    public Vector2 Position;
}

[Input, Unique]
public class RayCollisionComponent : IComponent
{
    public Vector2Int SlotPos;
    public Vector2? BoundPos;
    public Vector2 CollisionPos;
    public float Angle;
}

[Input, Unique]
public class SoundOnComponent : IComponent
{
    
}

[Input, Unique]
public class VibrationOnComponent : IComponent
{
    
}
