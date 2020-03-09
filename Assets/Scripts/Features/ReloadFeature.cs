public class ReloadFeature : Feature
{
    public ReloadFeature(Contexts contexts) : base("Reload Feature")
    {
        Add(new ReloadSystem(contexts));
        Add(new CompleteReloadSystem(contexts));
    }
}