using Code.Hero.Abilities;
using UnityEngine;

namespace Code.UI.Elements.AbilityPanel
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private AbilityType _abilityType;
        
        public AbilityType AbilityType => _abilityType;
        
        public void Show() => 
            gameObject.SetActive(true);
        
        public void Hide() => 
            gameObject.SetActive(false);
    }
}