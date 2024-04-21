namespace Source.Title
{
    public interface ISettableVisibility
    {
        public void SetIsVisible(bool to);
        public bool IsVisible { get; }
    }
}