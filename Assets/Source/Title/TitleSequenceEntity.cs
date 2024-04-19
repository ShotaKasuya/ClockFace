using R3;

namespace Source.Title
{
    public class TitleSequenceEntity
    {
        public readonly ReactiveProperty<TitleSequence> TitleSequence = new ReactiveProperty<TitleSequence>(Title.TitleSequence.Title);
    }
}