using UnityEngine;
using Zenject;

public class ControlInstaller : MonoInstaller
{
    [SerializeField] private MyButton _gasButton;
    [SerializeField] private MyButton _breakButton;
    [SerializeField] private Rudder _rudder;

    public override void InstallBindings()
    {
        Container.Bind<MyButton>().WithId("gas").FromInstance(_gasButton);
        Container.Bind<MyButton>().WithId("break").FromInstance(_breakButton);

        Container.Bind<Rudder>().FromInstance(_rudder).AsSingle();
    }
}