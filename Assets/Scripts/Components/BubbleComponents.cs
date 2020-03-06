using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;

[Game]
public class ViewComponent : IComponent
{
    public GameObject GameObject;
    public RectTransform RectTransform;
    public Image Image;
    public Text Text;
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