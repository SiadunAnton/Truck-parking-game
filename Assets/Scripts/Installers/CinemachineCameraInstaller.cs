using UnityEngine;
using Zenject;
using Cinemachine;

public class CinemachineCameraInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    public override void InstallBindings()
    {
        Container.Bind<CinemachineVirtualCamera>().FromInstance(_camera).AsSingle();
    }
}