public class ScrollFeature : Feature
{
    public ScrollFeature(Contexts contexts) : base("Scroll Feature")
    {
        Add(new BubbleScrollSystem(contexts));
        Add(new BubbleScrollToSlotSystem(contexts));
        Add(new CompleteScrollSystem(contexts));
    }
}