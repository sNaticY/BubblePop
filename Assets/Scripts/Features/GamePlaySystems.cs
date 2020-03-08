public class GamePlaySystems : Feature
{
    public GamePlaySystems(Contexts contexts) : base("GamePlay Systems")
    {
        Add(new PredictBubbleSystem(contexts));
        
        Add(new BubblePosToSlotSystem(contexts));
        Add(new BubbleMoveToSlotSystem(contexts));
        Add(new BubbleMoveToPosSystem(contexts));
        
        Add(new CreateBubbleInSlotSystem(contexts));
        Add(new DestroyBubbleSystem(contexts));

    }
}