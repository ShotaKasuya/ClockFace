using System;
using R3;
using VContainer;

namespace Source.Title.Entity
{
    public class TitleSequenceEntity:IDisposable
    {
        public ReactiveProperty<TitleSequence> TitleSequence { get; }
        
        [Inject]
        public TitleSequenceEntity()
        {
            TitleSequence = new ReactiveProperty<TitleSequence>();
        }
        
        public void Dispose()
        {
            TitleSequence?.Dispose();
        }
    }
}