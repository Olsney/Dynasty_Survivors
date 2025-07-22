using UnityEngine;
using Zenject;

namespace Code.Services.SceneProvider
{
    public class SceneContainer : MonoBehaviour
    {
        private ISceneProvider _sceneProvider;

        [Inject]
        public void Construct(ISceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }

        private void Awake()
        {
            _sceneProvider.SetTransform(transform);
        }
    }
}