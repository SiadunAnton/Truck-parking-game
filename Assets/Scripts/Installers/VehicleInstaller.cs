using UnityEngine;
using Zenject;

public class VehicleInstaller : MonoInstaller
{
    [SerializeField] private Rigidbody2D _truckRigidbody;
    [SerializeField] private Movement _movement;
    [SerializeField] private Gear _gear;

    public override void InstallBindings()
    {
        Container.Bind<Rigidbody2D>().WithId("truck").FromInstance(_truckRigidbody);

        Container.Bind<Movement>().FromInstance(_movement).AsSingle();
        Container.Bind<Gear>().FromInstance(_gear).AsSingle();
    }
}