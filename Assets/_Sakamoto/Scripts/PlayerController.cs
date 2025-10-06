using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = true;
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;
    private Vector2 _currentInput = Vector2.zero;

    private void Awake()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _playerData = GetComponent<PlayerData>();
        _playerMove = GetComponent<PlayerMove>();
        _playerJump = GetComponent<PlayerJump>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.PlayerMove.canceled += OnInputMove;
        _inputBuffer.PlayerJump.started += OnInputJump;
        _playerMove?.StartSetVariables(_playerData);
        _playerJump?.StartSetVariables(_playerData);
    } 

    private void OnDestroy()
    {
        _inputBuffer.PlayerMove.performed -= OnInputMove;
        _inputBuffer.PlayerMove.canceled -= OnInputMove;
        _inputBuffer.PlayerJump.started -= OnInputJump;
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded(_playerData);
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
}
