using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Slider _generalVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
    }
}