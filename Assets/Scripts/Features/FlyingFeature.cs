public class FlyingFeature : Feature
{
    public FlyingFeature(Contexts contexts) : base("Flying Feature")
    {
        Add(new CompleteFlying(contexts));
    }
}