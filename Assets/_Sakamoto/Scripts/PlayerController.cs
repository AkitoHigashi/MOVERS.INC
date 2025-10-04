using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerData))]
public class PlayerController : MonoBehaviour
{
    public bool IsGround { get; private set; } = true;
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;

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
        _inputBuffer.MoveAction.performed += OnInputMove;
        _inputBuffer.MoveAction.canceled += OnInputMove;
        _inputBuffer.JumpAction.started += OnInputJump;
        _playerMove?.StartSetVariables(_playerData);
        _groundCheck?.StartSetVariables(_playerData);
        _playerJump?.StartSetVariables(_playerData);
    }

    private void OnDestroy()
    {
        _inputBuffer.MoveAction.performed -= OnInputMove;
        _inputBuffer.MoveAction.canceled -= OnInputMove;
        _inputBuffer.JumpAction.started -= OnInputJump;
    }

    private void Update()
    {
        IsGround = _groundCheck.IsGrounded(_playerData);
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _playerMove?.Move(input, _playerData);
        }
        else if (context.canceled)
        {
            _playerMove?.Stop();
        }
    }

    private void OnInputJump(InputAction.CallbackContext context)
    {
        if (IsGround)
        {
            _playerJump?.Jump();
        }
    }
}
