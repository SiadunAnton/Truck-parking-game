using UnityEngine;
using Zenject;

public class GameManagmentInstaller : MonoInstaller
{
    [SerializeField] private LevelBehavior _levelBehaviour;

    public override void InstallBindings()
    {
        Container.Bind<LevelBehavior>().FromInstance(_levelBehaviour).AsSingle();
    }
}
