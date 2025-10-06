using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform _cameraForward;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;

    [Header("Move")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    
    public Transform CameraForward => _cameraForward;
    public LayerMask GroundLayer => _groundLayer;
    public float PlayerHeight => _playerHeight;
    public float WalkSpeed => _walkSpeed;

    public float SprintSpeed => _sprintSpeed;
    public float JumpForce => _jumpForce;
}
