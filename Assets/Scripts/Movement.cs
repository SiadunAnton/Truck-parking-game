using System.Collections;
using UnityEngine;
using Zenject;

public class Movement : MonoBehaviour
{
    public float Acceleration => _acceleration;
    public float CurrentGear => _gear.Current;
    public bool MoveBackward => _gear.Current < 0;

    [Header("Rotation")]
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private float _rotationSpeed;

    [Header("Power params")]
    [SerializeField] private float _powerOfTruck;
    [SerializeField] private float _weightOfCargo;

    [Header("Wheels")]
    [SerializeField] private Transform _leftWheel;
    [SerializeField] private Transform _rightWheel;

    //Control
    private Gear _gear;
    private Rudder _rudder;
    private MyButton _gasButton;
    private MyButton _breakButton;

    private Rigidbody2D _rb;

    //Acceleration variables
    private float _acceleration;
    private float _accelerationGap = 0.15f;

    //Control state
    private bool _gasPressed;
    private bool _breakPressed;

    [Inject]
    public void Initialize(Rudder rudder, Gear gear,[Inject(Id = "gas")]MyButton gasButton, [Inject(Id = "break")] MyButton breakButton)
    {
        _rudder = rudder;
        _gear = gear;
        _gasButton = gasButton;
        _breakButton = breakButton;
    }

    private void Awake()
    {     
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AccelerationProcess());

        _gasButton.onDown.AddListener( () => _gasPressed = true);
        _gasButton.onUp.AddListener(() => _gasPressed = false);
        _breakButton.onDown.AddListener(() => _breakPressed = true);
        _breakButton.onUp.AddListener(() => _breakPressed = false);
    }

    private void FixedUpdate()
    {
        var gearRatio =  _gear.GetRatio();
        if (_rb.velocity.magnitude > 0.05f) 
            transform.RotateAround( _connectPoint.position,
                                    Vector3.forward,
                                    Mathf.Sign(gearRatio) * CalculateRotationAngle() * Time.fixedDeltaTime);
        
        _rb.velocity = Vector2.Lerp(_rb.velocity,
                                    CalculateVelocity() * gearRatio  * Time.fixedDeltaTime,
                                    0.02f);

        RotateWheels();
    }

    private float CalculateRotationAngle()
    {
        var sensetivity = PlayerPrefs.GetFloat("sensetivity");
        var velocityMagnitude = _rb.velocity.magnitude;
        var rotation = (_rotationSpeed + sensetivity) * _rudder.GetHorizontal;
        var rotationInfluenceCoefficient = Mathf.Min(velocityMagnitude * 3f, 1);
        return rotationInfluenceCoefficient * rotation;
    }

    private Vector2 CalculateVelocity()
    {
        var powerOfMovement = _powerOfTruck - _weightOfCargo;
        return _acceleration * powerOfMovement * transform.up;
    }

    private void RotateWheels()
    {
        _leftWheel.localRotation = Quaternion.Euler(0f, 0f, _rudder.GetHorizontal * 25f);
        _rightWheel.localRotation = Quaternion.Euler(0f, 0f, _rudder.GetHorizontal * 25f);
    }


    IEnumerator AccelerationProcess()
    {
        var multiplicator = _powerOfTruck / 100 * 0.05f;
        for(; ; )
        {
            if (_gasPressed)
            {
                if(Acceleration < 1f)
                {
                    _acceleration += multiplicator;
                }
            }
            else
            {
                if(Acceleration > 0f)
                {
                    if (_breakPressed)
                    {
                        _acceleration = Mathf.Max(0f, Acceleration - 5 * multiplicator);
                    }
                    else
                        _acceleration = Mathf.Max(0f, Acceleration - multiplicator);
                }
            }
            yield return new WaitForSeconds(_accelerationGap);
        }
    }
}
