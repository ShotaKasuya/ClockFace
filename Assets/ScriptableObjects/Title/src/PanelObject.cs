using System;
using Source;
using Source.Title;
using Source.Title.View;
using UnityEngine;

namespace ScriptableObjects.Title
{
    [CreateAssetMenu(fileName = nameof(PanelObject), menuName = "ScriptableObject/PanelObject")]
    public class PanelObject: ScriptableObject
    {
        public GameObject Panel => panel;
        public TitleSequence Sequence => sequence;

        public ISettableVisibility GetInstance()
        {
            var instance = Instantiate(panel);
            if (panel.TryGetComponent(out ISettableVisibility view))
            {
                return view;
            }
            else
            {
                Debug.LogError("View Not Set");
                return null;
            }
        }
        
        [SerializeField] private GameObject panel;
        [SerializeField] private TitleSequence sequence;
        
        
    }
}