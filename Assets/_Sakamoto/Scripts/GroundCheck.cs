using UnityEngine;

public class GroundCheck : MonoBehaviour, IStartSetVariables
{
    [SerializeField] private float _halfBody = 0.5f;
    private float _playerHeight;
    private LayerMask _groundLayer;
    public void StartSetVariables(PlayerData playerData)
    {
        _playerHeight = playerData.PlayerHeight;
        _groundLayer = playerData.GroundLayer;
    }

    public bool IsGrounded(PlayerData playerData)
    {
        return Physics.Raycast(transform.position, Vector3.down,
            _playerHeight * _halfBody + playerData.GroundCheckDistance, _groundLayer);
    }
}
