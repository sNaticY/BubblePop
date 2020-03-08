public class ScrollFeature : Feature
{
    public ScrollFeature(Contexts contexts) : base("Scroll Feature")
    {
        Add(new BubbleScrollSystem(contexts));
    }
}