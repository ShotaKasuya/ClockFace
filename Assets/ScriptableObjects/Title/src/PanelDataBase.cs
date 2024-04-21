using System.Collections.Generic;
using Source;
using Source.Title;
using Source.Title.View;
using UnityEngine;

namespace ScriptableObjects.Title
{
    [CreateAssetMenu(fileName = nameof(PanelDataBase), menuName = "ScriptableObject/PanelDataBase")]
    public class PanelDataBase: ScriptableObject
    {
        [SerializeField] private List<PanelObject> panelObjects;

        public ISettableVisibility GetPanel(TitleSequence sequence)
        {
            return panelObjects.Find(x => x.Sequence == sequence)?.GetInstance();
        }
    }
}