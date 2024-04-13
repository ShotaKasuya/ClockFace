using System.Collections.Generic;
using UnityEngine;

namespace Source.ScriptableObjectDefinition
{
    [CreateAssetMenu(fileName = nameof(SubMenuDataBase), menuName = "ScriptableObject/SubMenuDataBase")]
    public class SubMenuDataBase: ScriptableObject
    {
        public List<int> SubMenu => subMenu;
        [SerializeField] private List<int> subMenu;
    }
}