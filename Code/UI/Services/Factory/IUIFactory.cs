using Code.Infrastructure.Services;

namespace Code.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
        void Cleanup();
        void CreateGameOverWindow();
        void CreateSettingsWindow();
        void CreateAuthorWindow();
        void CreateMainMenu();
        void CreateAbilityChoiceWindow();
        void CreateGamePauseWindow();
    }
}
