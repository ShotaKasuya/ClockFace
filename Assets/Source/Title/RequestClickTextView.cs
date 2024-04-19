using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Title
{
    public class RequestClickTextView: MonoBehaviour, IGettableTransform
    {
        public Text ModelText => modelText;
        public Transform ModelTransform => _modelTransform;
        
        [SerializeField] private Text modelText;
        private Transform _modelTransform;

        private void Awake()
        {
            _modelTransform = this.transform;
            modelText = GetComponent<Text>();
        }
    }
}