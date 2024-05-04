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

        [Inject]
        public PanelSequenceLogic(List<TitlePanelView> settableVisibilities, TitleSequenceEntity titleSequenceEntity)
        {
            BuildPanels(settableVisibilities);
            
            //Advise: このままだと、ReactiveProperty（TitleSequence）が生き続ける限り、無限に購読（Subscribe）し続ける。解決方法は以下2通り。
            //1. AddToで CompositeDisposable or GameObject or Componentの寿命に紐づけることができる。
            //2. Subscribeの返り値がIObservableなので、それをメンバ変数として保持しておく。
            //   IDisposableを実装するとDispose()メソッドの実装が強制されるので、そこで保持しておいた変数のDispose()を記述する。
            titleSequenceEntity.TitleSequence
                .Pairwise()
                .Subscribe(x =>
                {
                    var panelBefore = _instancedPanels.FirstOrDefault(panel => panel.Item1 == x.Previous).Item2;
                    var panelAfter = _instancedPanels.FirstOrDefault(panel => panel.Item1 == x.Current).Item2;
                    panelBefore.ReverseVisible(false);
                    panelAfter.ReverseVisible(true);
                });
            Debug.Log($"current {titleSequenceEntity.TitleSequence.Value}");
        }

        //Advise: Arrayだと広義かな。今回はPanelの初期化しかしてないので、Panelsってメソッド名に入れていいと思う。
        private void BuildPanels(List<TitlePanelView> settableVisibilities)
        {
            //Advise: Listは初期化時に数の指定ができるので、ある程度数が決まってるなら指定しておくのがオススメ。
            _instancedPanels = new List<(TitleSequence, ISettableVisibility)>(Enum.GetValues(typeof(TitleSequence)).Length);

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
                    //Question: これ初期化に見えるけど、もし、viewのactiveSelfが最初trueだったらactive状態は切り替わらないよね？
                    //つまり、あらかじめシーン上で設定したアクティブ状態に影響を受けるって事。そこは大丈夫そう？
                    view.ReverseVisible(true);
                }
                else
                {
                    //Advise: 上記コメントと同じく
                    view.ReverseVisible(false);
                }
            }
        }
    }
}