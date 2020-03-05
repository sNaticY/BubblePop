public class GamePlaySystems : Feature
{
    public GamePlaySystems(Contexts contexts) : base("Movement Systems")
    {
        Add(new LoadSettingsSystem(contexts));
        
        Add(new CreateBubblesSystem(contexts));
        Add(new MoveSystem(contexts));
        
    }
}