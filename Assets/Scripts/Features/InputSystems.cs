using Entitas;

public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input Systems")
    {
        Add(new EmitInputSystem(contexts));
        Add(new RayCastSystem(contexts));
        Add(new FireBubbleSystem(contexts));
    }         
}