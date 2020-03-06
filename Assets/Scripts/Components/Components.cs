using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;

[Game]
public class MoveComponent : IComponent
{
    public Vector2 target;
}

[Game]
public class MoveCompleteComponent : IComponent
{
}

[Game]
public class PositionComponent : IComponent
{
    public Vector2 value;
}

[Game]
public class DirectionComponent : IComponent
{
    public float value;
}
