using UnityEngine;

namespace Source.Title.View
{
    public class TitlePanelView: MonoBehaviour, ISettableVisibility
    {
        public TitleSequence PanelSequence => panelSequence;
        public bool IsVisible => gameObject.activeSelf;
        [SerializeField] private TitleSequence panelSequence;
        
        public void SetIsVisible(bool to)
        {
            if (to != IsVisible)
            {
                gameObject.SetActive(to);
            }
        }
    }
}