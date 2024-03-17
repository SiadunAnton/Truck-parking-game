using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private List<Collider2D> _parkingObjects;

    private IPlayerInform _informer;
    private LevelBehavior _levelBehaviour;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Bounds _bounds;
    private bool _won = false;

    [Inject]
    public void Initialize([Inject(Id = "truck")] Rigidbody2D rb, LevelBehavior levelBehavior, IPlayerInform informer)
    {
        _rb = rb;
        _levelBehaviour = levelBehavior;
        _informer = informer;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _bounds = _collider.bounds;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trailer") && _rb.velocity == Vector2.zero && !_won)
        {
            if (WasParkedRight())
            {
                _informer.InformWinEvent();
                _won = true;
                Invoke("Restart", 5f);
            }
        }
    }

    private bool WasParkedRight()
    {
        foreach (var parking in _parkingObjects)
        {
            if (!_bounds.ContainBound(parking.bounds))
                return false;
        }
        return true;
    }

    private void Restart()
    {
        _levelBehaviour.MoveToTheNextLevel();
    }
}
