using System.Runtime.CompilerServices;
using UnityEngine;

public class BladeTrapManeger : TrapBase
{
    [SerializeField]private GameObject _gameObject;
    [SerializeField]private float _bladeTrapXSpeed = 10f;
    [SerializeField]private float _bladeMoveSpeed = 1.0f;
    [SerializeField] private float _bladeReturnNum = 100;
    private float _time;

    private void Start()
    {
       Vector3 ReturnNum = new Vector3(_bladeReturnNum + _gameObject.transform.position.x, _gameObject.transform.position.z, _gameObject.transform.position.x);
    }

    private void Update()
    {
        BladeRollingMove();
    }

    private void BladeRollingMove() 
    {
        if (_time > 1)
        {
            _time = 0;
        }
        _time +=Time.deltaTime * _bladeMoveSpeed;
        _gameObject.transform.Rotate(_bladeTrapXSpeed, 0, 0);
        float sinValue = Mathf.Sin(2 * Mathf.PI * _time);
        transform.localPosition = new Vector3(0, 0, sinValue * _bladeReturnNum);
    }
}
