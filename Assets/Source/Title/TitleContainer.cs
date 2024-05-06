//Advise: いらないusingは消しましょう！
using System.Collections.Generic;
using Source.Title.Entity;
using Source.Title.Logic;
using Source.Title.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Title
{
    public class TitleContainer : LifetimeScope
    {
        //Advise: ~Baseって書くと基底クラスと勘違いされるので、データベースの時は Database にしてbを小文字にした方が良き。
        [SerializeField] private List<TitlePanelView> panelDataBase;
        [SerializeField] private RequestClickTextView requestClickText;
        [SerializeField] private DifficultyCursorView difficultyCursorView;
        [SerializeField] private List<DifficultyView> difficultyViews;
        
        //Advise: どうせこの後使わないなら変数として保持しておく必要ないかも。
        //private SelectDifficultyLogic _selectDifficultyLogic;
        //private PanelSequenceLogic _panelSequenceLogic;
        
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
            new SelectDifficultyLogic(difficultyCursorView, difficultyViews, sequence);
            new PanelSequenceLogic(panelDataBase, sequence);
        }
    }
}