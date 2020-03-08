
public class FireFeature : Feature
{
    public FireFeature(Contexts contexts) : base("Fire Feature")
    {
        Add(new FireBubbleSystem(contexts));
    }
}
