using System.Collections.Generic;
using Source.InGameScene.SubMenu;
using UnityEngine;

namespace ScriptableObjects.InGameScene.SubMenu
{
    [CreateAssetMenu(fileName = nameof(SubMenuDataBase), menuName = "ScriptableObject/SubMenuDataBase")]
    public class SubMenuDataBase: ScriptableObject
    {
        public List<SubMenuDescriptor> SubMenu => subMenu;
        [SerializeField] private List<SubMenuDescriptor> subMenu;
    }
}