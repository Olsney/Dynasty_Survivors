using System;
using System.Collections.Generic;
using Code.Hero.Abilities;
using Code.Services.Ability;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements.AbilityPanel
{
    public class AbilityPanelView : MonoBehaviour
    {
        [SerializeField] private List<AbilityView> _abilities;

        private IAbilityProvider _abilityProvider;
        private Dictionary<AbilityType, AbilityView> _abilityViews;

        [Inject]
        private void Construct(IAbilityProvider abilityProvider)
        {
            _abilityProvider = abilityProvider;
        }

        public void Initialize()
        {
            InitializeAbilityViews();
            HideAll();

            _abilityProvider.AbilityAdded += OnAbilityAdded;

            foreach (HeroAbility heroAbility in _abilityProvider.GetAbilities())
            {
                if (_abilityViews.TryGetValue(heroAbility.AbilityType, out AbilityView abilityView))
                    abilityView.Show();
            }
        }

        private void OnDestroy()
        {
            if (_abilityProvider != null)
                _abilityProvider.AbilityAdded -= OnAbilityAdded;
        }

        private void InitializeAbilityViews()
        {
            _abilityViews = new Dictionary<AbilityType, AbilityView>();

            foreach (AbilityView ability in _abilities)
                _abilityViews[ability.AbilityType] = ability;

        }

        private void HideAll()
        {
            foreach (AbilityView abilityView in _abilities) 
                abilityView.Hide();
        }

        private void OnAbilityAdded(HeroAbility heroAbility)
        {
            Show(heroAbility.AbilityType);
        }

        private void Show(AbilityType abilityType)
        {
            if (_abilityViews.TryGetValue(abilityType, out AbilityView abilityView))
                abilityView.Show();
        }
    }
}