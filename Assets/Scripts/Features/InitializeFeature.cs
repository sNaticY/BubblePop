namespace Features
{
    public class InitializeFeature : Feature
    {
        public InitializeFeature(Contexts contexts) : base("Initialize Systems")
        {
            Add(new LoadSettingsSystem(contexts));
            Add(new InitSlotBubblesSystem(contexts));
        }
    }
}