using Source.InGameScene.SubMenu;
using UnityEngine;

namespace ScriptableObjects.InGameScene.SubMenu
{
    [CreateAssetMenu(fileName = nameof(SubMenuPanelData), menuName = "ScriptableObject/SubMenuData")]
    public class SubMenuPanelData: ScriptableObject
    {
        public GameObject Panel => panel;
        public SubMenuDescriptor Descriptor => descriptor;

        [SerializeField] private GameObject panel;
        [SerializeField] private SubMenuDescriptor descriptor;
    }
}