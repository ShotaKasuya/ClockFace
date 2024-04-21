using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using ScriptableObjects.Title;
using Source.Title;
using Source.Title.Entity;
using Source.Title.Logic;
using Source.Title.View;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Source.Title
{
    public class TitleContainer : LifetimeScope
    {
        [SerializeField] private List<TitlePanelView> panelDataBase;
        [SerializeField] private RequestClickTextView requestClickText;
        [SerializeField] private DifficultyCursorView difficultyCursorView;
        [SerializeField] private List<DifficultyView> difficultyViews;

        private SelectDifficultyLogic _selectDifficultyLogic;
        private PanelSequenceLogic _panelSequenceLogic;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(requestClickText).As<IGettableTransform>();
            builder.RegisterEntryPoint<MoveSinVerticalLogic>();

            builder.Register<WaitClickEntity>(Lifetime.Singleton);
            builder.Register<TitleSequenceEntity>(Lifetime.Singleton);
            builder.RegisterEntryPoint<TitleSequenceLogic>(Lifetime.Singleton);

            builder.RegisterComponent(panelDataBase);
            builder.Register<PanelSequenceLogic>(Lifetime.Singleton);
        }

        private void Start()
        {
            var sequence = Container.Resolve<TitleSequenceEntity>();
            _selectDifficultyLogic = new SelectDifficultyLogic(difficultyCursorView, difficultyViews, sequence);
            _panelSequenceLogic = new PanelSequenceLogic(panelDataBase, sequence);
        }
    }
}