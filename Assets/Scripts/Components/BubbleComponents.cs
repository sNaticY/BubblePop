using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

[Game]
public class BubbleViewComponent : IComponent
{
    public GameObject GameObject;
    public RectTransform RectTransform;
    public Image Image;
    public Text Text;
    public Collider2D Collider;
    public GameObject TrailEffect;
    public Animation Animation;
}

[Game, Unique]
public class LineViewComponent : IComponent
{
    public UILineRenderer Line1;
    public UILineRenderer Line2;
}

[Game]
public class BubbleTargetSlotComponent : IComponent
{
    public Vector2? BoundPos;
    public Vector2 TargetPos;
    public Vector2Int TargetSlot;
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
public class ReadyToFire : IComponent
{
    
}

[Game]
public class PositionComponent : IComponent
{
    public Vector2 Value;
}

[Game]
public class BubbleSlotPosComponent : IComponent
{
    [PrimaryEntityIndex]
    public Vector2Int Value;
}

[Game]
public class NewBubbleInSlot : IComponent
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

[Game, Unique]
public class ReloadComponent : IComponent
{
    
}