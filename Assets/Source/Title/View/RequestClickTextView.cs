using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Title.View
{
    public class RequestClickTextView: MonoBehaviour, IGettableTransform
    {
        public Text ModelText => modelText;

        public Transform ModelTransform
        {
            get;
            private set;
        }
        
        [SerializeField] private Text modelText;

        private void Awake()
        {
            ModelTransform = this.transform;
            modelText = GetComponent<Text>();
        }
    }
}