using System;
using Code.UI.Services.Windows;
using Code.UI.Windows;
using UnityEngine.Serialization;

namespace Code.StaticData.Windows
{
    [Serializable]
    public class WindowConfig 
    {
        [FormerlySerializedAs("windowTypeId")] 
        [FormerlySerializedAs("WindowId")] 
        public WindowType windowType;
        public WindowBase Prefab;
    }
}