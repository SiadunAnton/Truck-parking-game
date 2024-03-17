using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }

}
