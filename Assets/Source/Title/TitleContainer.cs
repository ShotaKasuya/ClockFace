using System;
using System.Collections;
using System.Collections.Generic;
using R3;
using Source.Title;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class TitleContainer : LifetimeScope
{
    [SerializeField] private RequestClickTextView requestClickText;

    protected override void Configure(IContainerBuilder builder)
    {
        ObservableSystem.DefaultFrameProvider = new TimerFrameProvider(TimeSpan.FromSeconds(Time.fixedDeltaTime));

        builder.RegisterComponent(requestClickText).As<IGettableTransform>();
        builder.RegisterEntryPoint<MoveSinVerticalLogic>();

        builder.Register<WaitClickEntity>(Lifetime.Singleton);
        builder.Register<TitleSequenceEntity>(Lifetime.Singleton);
        builder.RegisterEntryPoint<TitleSequenceLogic>(Lifetime.Singleton);
    }

}
