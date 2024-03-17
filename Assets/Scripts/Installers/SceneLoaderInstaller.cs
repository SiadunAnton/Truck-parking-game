using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _loader;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(_loader).AsSingle();
    }
}