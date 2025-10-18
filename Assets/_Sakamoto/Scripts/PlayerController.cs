using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = true;
    public bool IsSprinting { get; private set; } = false;
    public bool IsCrouching { get; private set; } = false;
    public bool IsSliding { get; set; } = false;
    public bool IsCarrying { get; private set; } = false;
    public bool IsThrowing { get; private set; } = false;
    public bool CanSliding { get; private set; } = false;
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerState _playerState;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;
    private PlayerSprint _playerSprint;
    private PlayerCrouch _playerCrouch;
    private PlayerSliding _playerSliding;
    private PlayerCarry _playerCarry;
    private PlayerThrow _playerThrow;
    private Interact _interact;
    private PlayerHealth _playerHealth;
    private Vector2 _currentInput = Vector2.zero;

    private void Awake()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _playerData = GetComponent<PlayerData>();
        _playerState = GetComponent<PlayerState>();
        _playerMove = GetComponent<PlayerMove>();
        _playerJump = GetComponent<PlayerJump>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerSprint = GetComponent<PlayerSprint>();
        _playerCrouch = GetComponent<PlayerCrouch>();
        _playerSliding = GetComponent<PlayerSliding>();
        _playerCarry = GetComponent<PlayerCarry>();
        _playerThrow = GetComponent<PlayerThrow>();
        _interact = GetComponent<Interact>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.PlayerMove.canceled += OnInputMove;
        _inputBuffer.PlayerJump.started += OnInputJump;
        _inputBuffer.PlayerSprint.started += OnInputSprint;
        _inputBuffer.PlayerSprint.canceled += OnInputSprint;
        _inputBuffer.PlayerCrouch.started += OnInputCrouch;
        _inputBuffer.PlayerCarry.started += OnInputCarry;
        _inputBuffer.PlayerThrow.started += OnInputThrowAction;
        _inputBuffer.PlayerThrow.canceled += OnInputThrowAction;
        _inputBuffer.PlayerInteract.started += OnInputInteractAction;
        _inputBuffer.PlayerInteract.canceled += OnInputInteractAction;
        SetUp();
    }

    private void OnDestroy()
    {
        _inputBuffer.PlayerMove.performed -= OnInputMove;
        _inputBuffer.PlayerMove.canceled -= OnInputMove;
        _inputBuffer.PlayerJump.started -= OnInputJump;
        _inputBuffer.PlayerSprint.started -= OnInputSprint;
        _inputBuffer.PlayerSprint.canceled -= OnInputSprint;
        _inputBuffer.PlayerCrouch.started -= OnInputCrouch;
        _inputBuffer.PlayerCarry.started -= OnInputCarry;
        _inputBuffer.PlayerThrow.started -= OnInputThrowAction;
        _inputBuffer.PlayerThrow.canceled -= OnInputThrowAction;
        _inputBuffer.PlayerInteract.started -= OnInputInteractAction;
        _inputBuffer.PlayerInteract.canceled -= OnInputInteractAction;
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded(_playerData);
        UpdateReturnBool();
        UpdateCanBool();
        UpdateSetBool();
        _playerState.UpdateState(IsSprinting, IsCrouching, IsSliding, IsCarrying,IsThrowing);
        _playerMove?.UpdateSpeed(_playerState);
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _currentInput = input;
            _playerMove?.Move(input, _playerData);
        }
        else if (context.canceled)
        {
            _currentInput = Vector2.zero;
            _playerMove?.StopMove();
        }

    }

    private void OnInputJump(InputAction.CallbackContext context)
    {
        if (IsGrounded)
        {
            _playerJump?.Jump();
        }
    }

    private void OnInputSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (IsCrouching) return;
            _playerSprint?.StartSprint();
        }
        else if (context.canceled)
        {
            _playerSprint?.StopSprint();
        }
    }

    private void OnInputCrouch(InputAction.CallbackContext context)
    {
        if (CanSliding)
        {
            _playerSliding?.StartSliding();
        }
        else if (IsGrounded && !IsCrouching)
        {
            _playerCrouch?.StartCrouch();
        }
        else if (IsCrouching)
        {
            _playerCrouch?.StopCrouch();
        }
    }

    private void OnInputCarry(InputAction.CallbackContext context)
    {
        _playerCarry?.CarryAction();
    }

    private void OnInputThrowAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerThrow?.StartThrow();
        }
        else if (context.canceled)
        {
            _playerThrow?.StopThrow();
        }
    }

    private void OnInputInteractAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _interact?.StartInteract();
        }
        else if (context.canceled)
        {
            _interact?.StopInteract();
        }
    }

    /// <summary>
    /// 各種状態の更新のためのブール値の戻り値
    /// </summary>
    private void UpdateReturnBool()
    {
        IsSprinting = _playerSprint.ReturnIsSprint();
        IsCrouching = _playerCrouch.ReturnIsCrouch();
        IsSliding = _playerSliding.ReturnIsSliding();
        IsCarrying = _playerCarry.ReturnIsCarrying();
        IsThrowing = _playerThrow.ReturnIsThrowing();
    }

    /// <summary>
    /// 可能状態かどうか判定するためのブール値の更新
    /// </summary>
    private void UpdateCanBool()
    {
        CanSliding = _playerSliding.CanSliding(IsCarrying, IsSprinting, IsGrounded, _currentInput);
    }

    /// <summary>
    /// 各種状態の更新のためのブール値のセット
    /// </summary>
    private void UpdateSetBool()
    {
        _playerMove?.SetBool(IsSliding);
        _playerThrow?.SetBoolIsCarry(IsCarrying);
    }

    private void SetUp()
    {
        _playerMove?.StartSetVariables(_playerData);
        _playerJump?.StartSetVariables(_playerData);
        _playerSprint?.StartSetVariables(_playerData);
        _playerCrouch?.StartSetVariables(_playerData);
        _playerSliding?.StartSetVariables(_playerData);
        _playerCarry?.StartSetVariables(_playerData);
        _playerThrow?.StartSetVariables(_playerData);
        _interact?.StartSetVariables(_playerData);
        _playerHealth?.StartSetVariables(_playerData);
    }
}
