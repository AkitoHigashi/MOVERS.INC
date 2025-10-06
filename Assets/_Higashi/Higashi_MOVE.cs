using UnityEngine;
/// <summary>
/// 仮でプレイヤーを動かすスクリプト
/// </summary>
public class Higashi_MOVE : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] private int _speed;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();//オブジェクトのRBを取得
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.linearVelocity = transform.forward * _speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _rb.linearVelocity = -transform.forward * _speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rb.linearVelocity = transform.right * _speed;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _rb.linearVelocity = -transform.right * _speed;
        }
    }
}
