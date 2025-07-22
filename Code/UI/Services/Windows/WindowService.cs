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

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
                case WindowId.GameOver:
                    _uiFactory.CreateGameOverWindow();
                    break;
                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;
                case WindowId.Settings:
                    _uiFactory.CreateSettingsWindow();
                    break;
                case WindowId.Author:
                    _uiFactory.CreateAuthorWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowId), windowId, null);
            }
        }
    }
}