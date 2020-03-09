public class MergeFeature : Feature
{
    public MergeFeature(Contexts contexts) : base("Merge Feature")
    {
        Add(new CheckMergeSystem(contexts));
        Add(new ProcessMergeSystem(contexts));
        Add(new CompleteMergeMoveSystem(contexts));
        Add(new BubbleExplodeSystem(contexts));
        Add(new BubbleDropDownSystem(contexts));
        Add(new CompleteMergeSystem(contexts));
    }
}