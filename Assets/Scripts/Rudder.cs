using UnityEngine;
using System.Linq;

public class Rudder : MonoBehaviour
{
    public float GetHorizontal { get {  return _averageAngle / 720; } }

    private Camera _camera;

    private Vector2 _startValue;
    private Vector2 _endValue;

    private float _averageAngle;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.touches.Length > 0)
        {
            _startValue = _endValue;

            var touches = GetTouchesFromRightSideOfScreen();
            if (touches.Length > 0)
            {
                var touch = touches[0];
                _endValue = (Vector2)_camera.ScreenToWorldPoint(touch.position) - (Vector2)_camera.ScreenToWorldPoint(transform.position);
            }

            var angleTouch = -Vector2.SignedAngle(_endValue, _startValue);

            if (Mathf.Abs(angleTouch) < 45)
            {
                _averageAngle += angleTouch;
                transform.Rotate(0f, 0f, angleTouch);
            }
        }
        else
        {
            _averageAngle = 0f;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private Touch[] GetTouchesFromRightSideOfScreen()
    {
        var touches = Input.touches.Where(x => x.position.x > Screen.width / 2 &&
                                         (x.phase == TouchPhase.Moved
                                         || x.phase == TouchPhase.Began));
        return touches.ToArray();
    }
}
