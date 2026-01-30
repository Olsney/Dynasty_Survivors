using Code.Hero.Abilities;

namespace Code.Services.Ability
{
    public interface IAbilityService
    {
        void HandleChosenAbility(AbilityType abilityType);
        bool CanChooseAbility(AbilityType abilityType);
        bool CanChooseAnyAbility();
    }
}