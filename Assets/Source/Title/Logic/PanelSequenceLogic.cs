using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using ScriptableObjects.Title;
using Source.Title.Entity;
using Source.Title.View;
using UnityEngine;
using VContainer;

namespace Source.Title.Logic
{
    public class PanelSequenceLogic
    {
        private List<(TitleSequence, ISettableVisibility)> _instancedPanels;
        // private PanelDataBase _panelDataBase;
        // private TitleSequenceEntity _entity;

        [Inject]
        public PanelSequenceLogic(List<TitlePanelView> settableVisibilities, TitleSequenceEntity titleSequenceEntity)
        {
            BuildArray(settableVisibilities);
            titleSequenceEntity.TitleSequence
                .Pairwise()
                .Subscribe(x =>
                {
                    var panelBefore = _instancedPanels.FirstOrDefault(panel => panel.Item1 == x.Previous).Item2;
                    var panelAfter = _instancedPanels.FirstOrDefault(panel => panel.Item1 == x.Current).Item2;
                    panelBefore.SetIsVisible(false);
                    panelAfter.SetIsVisible(true);
                });
            Debug.Log($"current {titleSequenceEntity.TitleSequence.Value}");
        }

        private void BuildArray(List<TitlePanelView> settableVisibilities)
        {
            _instancedPanels = new List<(TitleSequence, ISettableVisibility)>();

            foreach (TitleSequence sequence in Enum.GetValues(typeof(TitleSequence)))
            {
                var view = settableVisibilities.FirstOrDefault(x => x.PanelSequence == sequence);
                if (view is null)
                {
                    Debug.LogError($"Panel Not Set {sequence}");
                    continue;
                }
                _instancedPanels.Add((sequence, view));
                if (sequence == TitleSequence.Title)
                {
                    view.SetIsVisible(true);
                }
                else
                {
                    view.SetIsVisible(false);
                }
            }
        }
    }
}