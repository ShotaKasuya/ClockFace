namespace Source.Title
{
    public interface ISettableVisibility
    {
        public void ReverseVisible(bool to);
        public bool IsVisible { get; }
    }
}