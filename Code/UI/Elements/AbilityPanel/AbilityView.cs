using Code.Hero.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements.AbilityPanel
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private Image Icon;
        [SerializeField] private Image CooldownSlider;
        
        [SerializeField] private AbilityType AbilityType { get; set; }
    }
}