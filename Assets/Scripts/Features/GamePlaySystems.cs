public class GamePlaySystems : Feature
{
    public GamePlaySystems(Contexts contexts) : base("GamePlay Systems")
    {
        Add(new PredictBubbleSystem(contexts));
        
        Add(new ResetPositionToSlotSystem(contexts));
        Add(new FireToSlotSystem(contexts));
        Add(new MoveToPositionSystem(contexts));
        
        Add(new CreateBubbleSystem(contexts));
        Add(new DestroyBubbleSystem(contexts));

        Add(new ProgressionSystem(contexts));

    }
}