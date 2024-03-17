using UnityEngine;
using Zenject;

public class LevelBehavior : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Initialize(SceneLoader loader)
    {
        _sceneLoader = loader;
    }

    public void MoveToTheNextLevel()
    {
        _sceneLoader.LoadNext();
    }

    public void RepeatCurrentLevel()
    {
        _sceneLoader.LoadCurrent();
    }
}
