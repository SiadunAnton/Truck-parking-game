using UnityEngine;
using Zenject;

public class AudioSourcesInstaller : MonoInstaller
{
    [SerializeField] private AudioSource _backUp;
    [SerializeField] private AudioSource _break;
    [SerializeField] private AudioSource _engine;

    public override void InstallBindings()
    {
        Container.Bind<AudioSource>().WithId("backUp").FromInstance(_backUp);
        Container.Bind<AudioSource>().WithId("break").FromInstance(_break);
        Container.Bind<AudioSource>().WithId("engine").FromInstance(_engine);
    }
}