using UnityEngine;

namespace Source.ScriptableObjectDefinition
{
    [CreateAssetMenu(fileName = nameof(SubMenuPanelData), menuName = "ScriptableObject/SubMenuData")]
    public class SubMenuPanelData: ScriptableObject
    {
        public GameObject Panel => panel;
        
        [SerializeField] private GameObject panel;
    }
}