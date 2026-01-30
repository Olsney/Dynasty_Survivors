using System;
using Code.UI.Services.Factory;

namespace Code.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Unknown:
                    break;
                case WindowType.Shop:
                    _uiFactory.CreateShop();
                    break;
                case WindowType.GameOver:
                    _uiFactory.CreateGameOverWindow();
                    break;
                case WindowType.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;
                case WindowType.Settings:
                    _uiFactory.CreateSettingsWindow();
                    break;
                case WindowType.Author:
                    _uiFactory.CreateAuthorWindow();
                    break;
                case WindowType.AbilityChoice:
                    _uiFactory.CreateAbilityChoiceWindow();
                    break;
                case WindowType.GamePause:
                    _uiFactory.CreateGamePauseWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }
    }
}