using UnityEngine;

namespace Code.Services.SceneProvider
{
    public interface ISceneProvider
    {
        void SetTransform(Transform container);
        Transform Container { get; }
    }
}