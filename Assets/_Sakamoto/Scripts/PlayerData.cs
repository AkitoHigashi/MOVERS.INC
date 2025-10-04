using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Transform _cameraForward;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    public Transform CameraForward => _cameraForward;
    public float MoveSpeed => _moveSpeed;
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float JumpPower => _jumpPower;
    public float PlayerHeight => _playerHeight;
    public float GroundCheckDistance => _groundCheckDistance;
    public LayerMask GroundLayer => _groundLayer;
}
