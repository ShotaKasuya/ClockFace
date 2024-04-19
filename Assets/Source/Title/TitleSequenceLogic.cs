using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Source.Title
{
    public class TitleSequenceLogic: ITickable
    {
        private readonly TitleSequenceEntity _titleSequenceEntity;
        private readonly WaitClickEntity _waitClickEntity;
        public void Initialize()
        {
        }

        public void Tick()
        {
            if (_waitClickEntity.WaitKey)
            {
                Sequence();
            }
        }

        private void Sequence()
        {
            Debug.Log("called");
            SceneManager.LoadScene("InGame");
            // switch (_titleSequenceEntity.TitleSequence.Value)
            // {
            //     case TitleSequence.Title:
            //         _titleSequenceEntity.TitleSequence.Value = TitleSequence.SelectDifficulty;
            //         break;
            //     case TitleSequence.SelectDifficulty:
            //         SceneManager.LoadScene("InGame");
            //         break;
            // }
        }

        [Inject]
        public TitleSequenceLogic(TitleSequenceEntity titleSequenceEntity, WaitClickEntity waitClickEntity)
        {
            _titleSequenceEntity = titleSequenceEntity;
            _waitClickEntity = waitClickEntity;
        }
    }
}