using R3;

namespace Source.Title.Entity
{
    public class TitleSequenceEntity
    {
        public readonly ReactiveProperty<TitleSequence> TitleSequence = new ReactiveProperty<TitleSequence>(Source.TitleSequence.Title);
    }
}