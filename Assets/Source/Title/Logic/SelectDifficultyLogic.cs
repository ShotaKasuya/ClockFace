using System.Collections.Generic;
using System.Linq;
using R3;
using Source.Title.Entity;
using Source.Title.View;
using UnityEngine;

namespace Source.Title.Logic
{
    public class SelectDifficultyLogic
    {
        private readonly DifficultyCursorView _cursorView;
        private readonly List<DifficultyView> _difficultyViews;

        public SelectDifficultyLogic(DifficultyCursorView cursorView, List<DifficultyView> difficultyViews, TitleSequenceEntity titleSequenceEntity)
        {
            _cursorView = cursorView;
            _difficultyViews = difficultyViews;
            cursorView.InputEvent
                .Subscribe(titleSequenceEntity, (kinds, state) =>
                {
                    if (state.TitleSequence.Value != TitleSequence.SelectDifficulty)
                    {
                        return;
                    }
                    switch (kinds)
                    {
                        case InputKinds.Up:
                            DifficultySaver.MoreEasy();
                            SetCursor();
                            break;
                        case InputKinds.Down:
                            DifficultySaver.MoreHard();
                            SetCursor();
                            break;
                    }
                });
        }

        private void SetCursor()
        {
            var nextPosition = _cursorView.ModelTransform.position;
            var cursorTo = _difficultyViews.FirstOrDefault(x => x.Info == DifficultySaver.CurrentDifficulty);
            if (cursorTo is null)
            {
                Debug.LogError($"Not Set...{DifficultySaver.CurrentDifficulty}");
                return;
            }
            nextPosition.y = cursorTo.ModelTransform.position.y;
            _cursorView.ModelTransform.position = nextPosition;
        }
    }
}