using UnityEngine;

namespace Code.Services.SceneProvider
{
    public class SceneProvider : ISceneProvider
    {
        public Transform Container { get; private set; }

        public void SetTransform(Transform container)
        {
            Container = container;
        }
    }
}