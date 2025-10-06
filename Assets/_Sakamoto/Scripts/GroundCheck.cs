using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    /// <summary>
    /// �����̗]�T���������邽�߂̒l
    /// </summary>
    [SerializeField] private float _groundCheckDistance = 0.2f;
    /// <summary>
    /// �v���C���[�̔����̍��������߂邽�߂̒l
    /// </summary>
    private float _playerHalfHeight = 0.5f;
    public bool IsGrounded(PlayerData playerData)
    {
        return Physics.Raycast(transform.position, Vector3.down,
            playerData.PlayerHeight * _playerHalfHeight + _groundCheckDistance, playerData.GroundLayer);
    }
}
