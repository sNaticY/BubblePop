using Entitas;

public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        Add(new AddBubbleViewSystem(contexts));
        Add(new RenderPositionSystem(contexts));
        Add(new RenderBubbleSpriteSystem(contexts));
        Add(new RenderBubbleTextSystem(contexts));
        Add(new RenderBubbleScaleSystem(contexts));
        Add(new ColliderControlSystem(contexts));
        Add(new RenderPredictBubbleSystem(contexts));
        Add(new RenderLineViewSystem(contexts));
    }
}