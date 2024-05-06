using UnityEngine;
using UnityEngine.UI;

namespace Source.InGameScene.Cristal
{
    public class CrystalView :MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        private int _index;
        public int Index => _index;
        
        private int _number;
        public int Number => _number;
        
        private void Start()
        {
            button.onClick.AddListener(SetHand);
        }

        private void SetHand()
        {
            GameManager.Instance.SetHand(_index, _number);
        }

        public void Init(int index, int number)
        {
            _index = index;
            _number = number;
            text.text = number.ToString();
        }

        public bool CanDisable()
        {
            return button.interactable;
        }

        public void Disable()
        {
            button.interactable = false;
        }

    }
}