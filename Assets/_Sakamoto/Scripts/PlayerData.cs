using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _muscleStrength = 1f;

    [Header("Reference")]
    [SerializeField] private Transform _cameraForward;
    [SerializeField] private Transform _luggagePosition;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;

    [Header("Move")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private float _staminaRecoverySpeed = 2f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;

    [Header("Crouch")]
    [SerializeField] private float _crouchHeight;

    [Header("Sliding")]
    [SerializeField] private float _slidingForce;
    [SerializeField] private float _slidingMaxTime;
    [SerializeField] private float _slidingCoolTime;
    [SerializeField] private float _slidingYScale;

    [Header("Carry")]
    [SerializeField] private float _carryRayDistance;
    [SerializeField] private string _luggageTag = "Luggage";

    [Header("Throw")]
    [SerializeField] private float _throwableTime;
    [SerializeField] private float _throwForceForward;
    [SerializeField] private float _throwForceUp;

    [Header("Interact")]
    [SerializeField] private float _interactTime = 2f;
    [SerializeField] private float _interactDistance = 3f;

    public float Health => _health;
    public float MuscleStrength => _muscleStrength;
    public Transform CameraForward => _cameraForward;
    public Transform LuggagePosition => _luggagePosition;
    public LayerMask GroundLayer => _groundLayer;
    public float PlayerHeight => _playerHeight;
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float CrouchSpeed => _crouchSpeed;
    public float SlidingSpeed => _slidingSpeed;
    public float Stamina => _stamina;
    public float StaminaRecoverySpeed => _staminaRecoverySpeed;
    public float JumpForce => _jumpForce;
    public float CrouchHeight => _crouchHeight;
    public float SlidingForce => _slidingForce;
    public float SlidingTimer => _slidingMaxTime;
    public float SlidingCoolTime => _slidingCoolTime;
    public float SlidingYScale => _slidingYScale;
    public float CarryRayDistance => _carryRayDistance;
    public string LuggageTag => _luggageTag;
    public float ThrowableTime => _throwableTime;
    public float ThrowForceForward => _throwForceForward;
    public float ThrowForceUp => _throwForceUp;
    public float InteractTime => _interactTime;
    public float InteractDistance => _interactDistance;
}
