public class ScrollFeature : Feature
{
    public ScrollFeature(Contexts contexts) : base("Scroll Feature")
    {
        Add(new BubbleScrollSystem(contexts));
        Add(new ScrollToSlotSystem(contexts));
        Add(new CompleteScrollSystem(contexts));
    }
}