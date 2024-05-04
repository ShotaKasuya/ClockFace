using UnityEngine;

namespace Source.Title.View
{
    public class TitlePanelView: MonoBehaviour, ISettableVisibility
    {
        public TitleSequence PanelSequence => panelSequence;
        public bool IsVisible => gameObject.activeSelf;
        [SerializeField] private TitleSequence panelSequence;
        
        //Advise: 必ず引数に指定した物がSetされる訳ではないので、SetよりReverseとかにした方がいいかも？
        public void ReverseVisible(bool to)
        {
            if (to != IsVisible)
            {
                gameObject.SetActive(to);
            }
        }
    }
}