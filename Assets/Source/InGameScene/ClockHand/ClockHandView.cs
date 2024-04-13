using UnityEngine;

namespace Source.InGameScene.ClockHand
{
    public class ClockHandView : MonoBehaviour
    {
        public Transform ModelTransform => modelTransform;
        [SerializeField] private Transform modelTransform;
    }
}