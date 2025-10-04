using UnityEngine;

public class PlayerJump : MonoBehaviour, IStartSetVariables
{
    private Rigidbody _rb;
    private float _jumpPower;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _jumpPower = playerData.JumpPower;
    }
}
