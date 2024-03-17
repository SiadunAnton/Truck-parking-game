using UnityEngine;
using Zenject;

public class SoundBinder : MonoBehaviour
{
    private AudioSource _backUp;
    private AudioSource _engine;
    private AudioSource _break;
    private Movement _movement;
    private MyButton _breakButton;

    [Inject]
    public void Initialize([Inject(Id = "backUp")]AudioSource backUpSource, [Inject(Id = "engine")]AudioSource engineSource,
                            [Inject(Id = "break")]AudioSource breakSource, Movement movement, [Inject(Id = "break")]MyButton breakButton )
    {
        _backUp = backUpSource;
        _engine = engineSource;
        _break = breakSource;
        _movement = movement;
        _breakButton = breakButton;
    }

    private void Start()
    {
        _breakButton.onDown.AddListener(() => { _break.Play(); });
        _breakButton.onUp.AddListener(() => { _break.Stop(); });

        SetupAudioSources();
    }

    private void SetupAudioSources()
    {
        _backUp.Play();
        _engine.Play();
        _backUp.mute = true;
        _engine.mute = false;
    }

    private void FixedUpdate()
    {
        _engine.pitch = 0.75f + _movement.Acceleration / 5 + Mathf.Abs(_movement.CurrentGear)/20;

        if( _movement.MoveBackward)
        {
            _backUp.mute = false;
        }
        else
        {
            _backUp.mute = true;
        }
    }

    private void OnDestroy()
    {
        _backUp.Stop();
        _engine.Stop();
    }
}
