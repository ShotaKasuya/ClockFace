using R3;
using Source.Title.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Source.Title.Logic
{
    public class TitleSequenceLogic: ITickable
    {
        private readonly TitleSequenceEntity _titleSequenceEntity;
        private readonly WaitClickEntity _waitClickEntity;

        public void Tick()
        {
            if (_waitClickEntity.WaitKey)
            {
                Sequence(_titleSequenceEntity.TitleSequence.Value);
            }
        }

        private void Sequence(TitleSequence current)
        {
            switch (current)
            {
                case TitleSequence.Title:
                    _titleSequenceEntity.TitleSequence.Value = TitleSequence.SelectDifficulty;
                    break;
                case TitleSequence.SelectDifficulty:
                    SceneManager.LoadScene("InGame");
                    break;
            }
        }

        [Inject]
        public TitleSequenceLogic(TitleSequenceEntity titleSequenceEntity, WaitClickEntity waitClickEntity)
        {
            _titleSequenceEntity = titleSequenceEntity;
            _waitClickEntity = waitClickEntity;
        }
    }
}