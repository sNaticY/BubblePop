using Entitas;
using UnityEngine;

[Game]
public class BubbleSlotPosComponent : IComponent
{
    public Vector2Int Value;
}

[Game]
public class BubbleIndexComponent : IComponent
{
    public int Value;
}

[Game]
public class BubbleNumberComponent : IComponent
{
    public int Value;
}