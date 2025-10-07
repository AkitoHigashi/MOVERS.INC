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
<<<<<<< HEAD
=======
    [SerializeField] private float _crouchSpeed;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private float _staminaRecoverySpeed = 2f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;

<<<<<<< HEAD
=======
    [Header("Crouch")]
    [SerializeField] private float _couchHeight;

>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    public Transform CameraForward => _cameraForward;
    public LayerMask GroundLayer => _groundLayer;
    public float PlayerHeight => _playerHeight;
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
<<<<<<< HEAD
    public float Stamina => _stamina;
    public float StaminaRecoverySpeed => _staminaRecoverySpeed;
    public float JumpForce => _jumpForce;
=======
    public float CrouchSpeed => _crouchSpeed;
    public float Stamina => _stamina;
    public float StaminaRecoverySpeed => _staminaRecoverySpeed;
    public float JumpForce => _jumpForce;
    public float CouchHeight => _couchHeight;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
}
