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
}

[Game, Unique]
public class LineViewComponent : IComponent
{
    public UILineRenderer Line1;
    public UILineRenderer Line2;
}



[Game]
public class BubbleSlotPosComponent : IComponent
{
    [PrimaryEntityIndex]
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