using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform _cameraForward;

    [Header("Move")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpForce;
    
    public Transform CameraForward => _cameraForward;
    public float WalkSpeed => _walkSpeed;

    public float SprintSpeed => _sprintSpeed;
    public float JumpForce => _jumpForce;
}
