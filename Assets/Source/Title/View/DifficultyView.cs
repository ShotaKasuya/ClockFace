using System;
using UnityEngine;

namespace Source.Title.View
{
    public class DifficultyView: MonoBehaviour
    {
        public Transform ModelTransform
        {
            get;
            private set;
        }

        public Difficulty Info => info;
        [SerializeField] private Difficulty info;

        private void Awake()
        {
            ModelTransform = transform;
        }
    }
}