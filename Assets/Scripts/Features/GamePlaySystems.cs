public class GamePlaySystems : Feature
{
    public GamePlaySystems(Contexts contexts) : base("Movement Systems")
    {
        Add(new LoadSettingsSystem(contexts));
        
        Add(new CreateBubblesSystem(contexts));
        Add(new BubblePosToSlotSystem(contexts));
        Add(new PrepareLaunchSystem(contexts));
        Add(new PredictBubbleSystem(contexts));
        Add(new BubbleMoveToSlotSystem(contexts));
        Add(new BubbleMoveToPosSystem(contexts));
        Add(new BubbleCheckMergeSystem(contexts));
        Add(new BubbleMergeSystem(contexts));
        Add(new BubbleCompleteMoveSystem(contexts));

    }
}