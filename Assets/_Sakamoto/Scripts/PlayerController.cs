using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = true;
    public bool IsSprinting { get; private set; } = false;
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerState _playerState;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;
    private PlayerSprint _playerSprint;
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
    }

    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.PlayerMove.canceled += OnInputMove;
        _inputBuffer.PlayerJump.started += OnInputJump;
        _inputBuffer.PlayerSprint.started += OnInputSprint;
        _inputBuffer.PlayerSprint.canceled += OnInputSprint;
        SetUp();
    } 

    private void OnDestroy()
    {
        _inputBuffer.PlayerMove.performed -= OnInputMove;
        _inputBuffer.PlayerMove.canceled -= OnInputMove;
        _inputBuffer.PlayerJump.started -= OnInputJump;
        _inputBuffer.PlayerSprint.started -= OnInputSprint;
        _inputBuffer.PlayerSprint.canceled -= OnInputSprint;    
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded(_playerData);
        UpdateReturnBool();
        _playerState.UpdateState(IsSprinting);
        _playerMove?.UpdateSpeed(_playerState);
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        if(context.performed)
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
            _playerSprint?.StartSprint();
        }
        else if (context.canceled)
        {
            _playerSprint?.StopSprint();
        }
    }

    private void UpdateReturnBool()
    {
        IsSprinting=_playerSprint.ReturnIsSprint();
    }

    private void SetUp()
    {
        _playerMove?.StartSetVariables(_playerData);
        _playerJump?.StartSetVariables(_playerData);
        _playerSprint?.StartSetVariables(_playerData);
    }
}
