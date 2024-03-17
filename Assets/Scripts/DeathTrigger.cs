using UnityEngine;
using Zenject;

public class DeathTrigger : MonoBehaviour
{
    private IPlayerInform _informer;
    private LevelBehavior _levelBehaviour;

    [Inject]
    public void Initialize(LevelBehavior levelBehavior, IPlayerInform informer)
    {
        _levelBehaviour = levelBehavior;
        _informer = informer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Solid"))
        {
            _informer.InformLoseEvent();
            Time.fixedDeltaTime = 0f;
            Invoke("Restart", 5f);
        }
    }

    private void Restart()
    {
        Time.fixedDeltaTime = 0.02f;
        _levelBehaviour.RepeatCurrentLevel();
    }
}
