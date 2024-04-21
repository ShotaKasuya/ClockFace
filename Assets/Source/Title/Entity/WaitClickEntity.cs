using R3;
using UnityEngine;
using VContainer.Unity;

namespace Source.Title.Entity
{
    public class WaitClickEntity
    {
        public bool WaitKey => Input.GetKeyDown(KeyCode.Mouse0) | Input.GetKeyDown(KeyCode.Space);
    }
}