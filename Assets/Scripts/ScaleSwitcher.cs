using System.Collections;
using UnityEngine;

public class ScaleSwitcher : MonoBehaviour
{
    [SerializeField] private Vector3 _gap = new Vector3(0.003f, 0.003f, 1f);
    [SerializeField] private float _tick = 0.05f;

    private Vector3 _scale;
    private bool _reverse = false;

    private void Awake()
    {
        _scale = transform.localScale;
    }

    private void Start()
    {
        StartCoroutine(SwitchProcess());
    }

    IEnumerator SwitchProcess()
    {
        for(; ; )
        {
            for(int i = 0; i < 10; i++)
            {
                if(!_reverse)
                {
                    _scale += _gap;
                }
                else
                {
                    _scale -= _gap;
                }
                transform.localScale = _scale;
                yield return new WaitForSeconds(_tick);
            }
            _reverse = _reverse ? false : true;
        }
    }
}
