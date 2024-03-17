using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Zenject;

public class CameraScale : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Slider _slider;

    [Inject]
    public void Initialize(CinemachineVirtualCamera camera)
    {
        _camera = camera;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(RefreshSize);
    }

    private void RefreshSize(float x)
    {
        _camera.m_Lens.OrthographicSize = 3 + x * 3;
    }
}
