using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[Game]
public class BubbleCreationComponent : IComponent
{
    public int Number;
    public Vector2Int Slot;
}

[Game]
public class BubbleViewComponent : IComponent
{
    public BubbleController Value;
}

[Game, Unique]
public class LineViewComponent : IComponent
{
    public UILineRenderer Line1;
    public UILineRenderer Line2;
}

[Game]
public class ConnectToCeil : IComponent
{
    
}

[Game]
public class ReadyToDropDown : IComponent
{
    
}

[Game]
public class ActiveBubbleComponent : IComponent
{
    
}

[Game]
public class BubbleTargetSlotComponent : IComponent
{
    public Vector2? BoundPos;
    public Vector2 TargetPos;
    public Vector2Int TargetSlot;
}

[Game]
public class BubbleScrollToSlotComponent : IComponent
{
    public Vector2Int Value;
}

[Game]
public class AnimationComponent : IComponent
{
    public float Delay;
    public string AnimationName;
}

[Game]
public class BubbleTargetPosComponent : IComponent
{
    public Vector2 Value;
}

[Game]
public class BubbleSecondTargetPosComponent : IComponent
{
    public Vector2 Value;
}

[Game]
public class CompleteMoveComponent : IComponent
{
    
}

[Game]
public class PositionComponent : IComponent
{
    public Vector2 Value;
}

[Game]
public class SpeedComponent : IComponent
{
    public float Value;
}

[Game]
public class BubbleSlotPosComponent : IComponent
{
    [PrimaryEntityIndex]
    public Vector2Int Value;
}

[Game, Unique]
public class NewBubbleComponent : IComponent
{
    
}

[Game]
public class ReadyToMerge : IComponent
{
    [EntityIndex]
    public int Number;
    public int TargetNumber;
    public Vector2Int TargetSlot;
}

[Game]
public class ReadyToDestroy : IComponent
{
    
}

[Game, Unique]
public class CurrentMergeNumber : IComponent
{
    public int Value;
}

[Game]
public class MergeableCheck : IComponent
{
    
}

[Game]
public class BubbleNumberComponent : IComponent
{
    public int Value;
}

[Game]
public class InSlotBubbleComponent : IComponent
{
    
}

[Game, Unique]
public class WaitingForLaunchComponent : IComponent
{
    
}

[Game, Unique]
public class NextLaunchComponent : IComponent
{
    
}

[Game, Unique]
public class PredictBubbleComponent : IComponent
{
    
}
