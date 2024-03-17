using UnityEngine;
using Zenject;

public class PlayerInformInstaller : MonoInstaller
{
    [SerializeField] private GameObject _informerHolder;

    public override void InstallBindings()
    {
        var informer = _informerHolder.GetComponent<IPlayerInform>();
        if (informer == null)
            throw new System.ArgumentNullException("Zenject exception: cannot bind IPlayerInfrom interface. Reference is empty.");
        Container.BindInterfacesAndSelfTo<IPlayerInform>().FromInstance(informer);
    }
}