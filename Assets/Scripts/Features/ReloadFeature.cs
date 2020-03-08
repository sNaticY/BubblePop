public class ReloadFeature : Feature
{
    public ReloadFeature(Contexts contexts) : base("Reload Feature")
    {
        Add(new PrepareLaunchSystem(contexts));
        Add(new CompleteReloadSystem(contexts));
    }
}