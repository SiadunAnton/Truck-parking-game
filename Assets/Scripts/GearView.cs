using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GearView : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Gear _gear;

    [Inject]
    public void Initialize(Gear gear)
    {
        _gear = gear;
    }

    private void Update()
    {
        if(_gear.Current == 0)
        {
            _text.text = "N";
            return;
        }
        _text.text = (_gear.Current).ToString();
    }


}
