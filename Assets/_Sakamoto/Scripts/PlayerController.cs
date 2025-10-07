<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = true;
    public bool IsSprinting { get; private set; } = false;
<<<<<<< HEAD
=======
    public bool IsCrouching { get; private set; } = false;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    private InputBuffer _inputBuffer;
    private PlayerData _playerData;
    private PlayerState _playerState;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;
    private PlayerSprint _playerSprint;
<<<<<<< HEAD
=======
    private PlayerCrouch _playerCrouch;
    private PlayerSliding _playerSliding;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
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
<<<<<<< HEAD
=======
        _playerCrouch = GetComponent<PlayerCrouch>();
        _playerSliding = GetComponent<PlayerSliding>();
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    }

    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.PlayerMove.canceled += OnInputMove;
        _inputBuffer.PlayerJump.started += OnInputJump;
        _inputBuffer.PlayerSprint.started += OnInputSprint;
        _inputBuffer.PlayerSprint.canceled += OnInputSprint;
<<<<<<< HEAD
        SetUp();
    } 
=======
        _inputBuffer.PlayerCrouch.started += OnInputCrouch;
        SetUp();
    }
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905

    private void OnDestroy()
    {
        _inputBuffer.PlayerMove.performed -= OnInputMove;
        _inputBuffer.PlayerMove.canceled -= OnInputMove;
        _inputBuffer.PlayerJump.started -= OnInputJump;
        _inputBuffer.PlayerSprint.started -= OnInputSprint;
<<<<<<< HEAD
        _inputBuffer.PlayerSprint.canceled -= OnInputSprint;    
=======
        _inputBuffer.PlayerSprint.canceled -= OnInputSprint;
        _inputBuffer.PlayerCrouch.started -= OnInputCrouch;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded(_playerData);
        UpdateReturnBool();
<<<<<<< HEAD
        _playerState.UpdateState(IsSprinting);
=======
        _playerState.UpdateState(IsSprinting,IsCrouching);
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
        _playerMove?.UpdateSpeed(_playerState);
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
<<<<<<< HEAD
        if(context.performed)
=======
        if (context.performed)
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
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
<<<<<<< HEAD
=======
            if (IsCrouching) return;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
            _playerSprint?.StartSprint();
        }
        else if (context.canceled)
        {
            _playerSprint?.StopSprint();
        }
    }

<<<<<<< HEAD
    private void UpdateReturnBool()
    {
        IsSprinting=_playerSprint.ReturnIsSprint();
=======
    private void OnInputCrouch(InputAction.CallbackContext context)
    {
        if (IsGrounded && IsSprinting)
        {
            _playerSliding?.Sliding();
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

    private void UpdateReturnBool()
    {
        IsSprinting = _playerSprint.ReturnIsSprint();
        IsCrouching = _playerCrouch.ReturnIsCrouch();
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    }

    private void SetUp()
    {
        _playerMove?.StartSetVariables(_playerData);
        _playerJump?.StartSetVariables(_playerData);
        _playerSprint?.StartSetVariables(_playerData);
<<<<<<< HEAD
=======
        _playerCrouch?.StartSetVariables(_playerData);
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    }
}
