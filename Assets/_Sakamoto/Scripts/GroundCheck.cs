using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    /// <summary>
    /// 足元の余裕を持たせるための値
    /// </summary>
    [SerializeField] private float _groundCheckDistance = 0.2f;
    /// <summary>
    /// プレイヤーの半分の高さを求めるための値
    /// </summary>
    private float _playerHalfHeight = 0.5f;
    public bool IsGrounded(PlayerData playerData)
    {
        return Physics.Raycast(transform.position, Vector3.down,
            playerData.PlayerHeight * _playerHalfHeight + _groundCheckDistance, playerData.GroundLayer);
    }
}
