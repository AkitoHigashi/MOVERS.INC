using UnityEngine;

public class LuggageSpeed : MonoBehaviour
{
    private Vector3 _prevPosition;
    private Vector3 _currentPosition;
    private Vector3 _luggagevelocity;
    private float _totalSpeed;
    [SerializeField] private bool _ishave = false; //アイテムをもっているときだけ計算する
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _prevPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_ishave) TakeLuggageSpeed();
        _totalSpeed = _luggagevelocity.magnitude + _rb.linearVelocity.magnitude;

    }

    public void TakeLuggageSpeed()
    {

        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        _currentPosition = transform.position;

        _luggagevelocity = (_currentPosition - _prevPosition) / Time.deltaTime;

        Debug.Log(_luggagevelocity.magnitude);
        _prevPosition = _currentPosition;
    }



    public float GetTotalSpeed() => _totalSpeed;

}
