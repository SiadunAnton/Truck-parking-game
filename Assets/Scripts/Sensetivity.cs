using UnityEngine;
using UnityEngine.UI;

public class Sensetivity : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("sensetivity");
        _slider.onValueChanged.AddListener(SaveValue);
    }

    private void SaveValue(float value)
    {
        PlayerPrefs.SetFloat("sensetivity", value);
    }
}
