using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public int Current { get { return _current - 3; } }

    private int _current = 3;
    private List<float> _gearRatios = new List<float>{ -1f,-0.55f, -0.3f, 0f, 0.25f, 0.8f, 1.5f, 2f,4f};

    public float GetRatio()
    {
        return _gearRatios[_current];
    }

    public void UpGear()
    {
        var nextGear = _current + 1;
        if (nextGear < _gearRatios.Count)
            _current = nextGear;
    }

    public void DownGear()
    { 
        var nextGear = _current - 1;
        if (nextGear >= 0)
           _current = nextGear;
        
    }

}
