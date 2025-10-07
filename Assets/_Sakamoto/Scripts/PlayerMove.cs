using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IStartSetVariables
{
    private Rigidbody _rb;
    private Transform _cameraForward;
    private float _moveSpeed;
    private float _walkSpeed;
    private float _sprintSpeed;
    private float _crouchSpeed;
    private Vector2 _currentInput;
    private Vector3 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_cameraForward == null) return;
        Vector3 inputDir = _cameraForward.forward * _currentInput.y + _cameraForward.right * _currentInput.x;
        inputDir.y = 0;
        _moveDirection = inputDir;
        GroundMove();
        SpeedControl();
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _walkSpeed = playerData.WalkSpeed;
        _sprintSpeed = playerData.SprintSpeed;
        _crouchSpeed = playerData.CrouchSpeed;
    }

    public void Move(Vector2 input, PlayerData playerData)
    {
        _currentInput = input;
        _cameraForward = playerData.CameraForward;
    }

    public void StopMove()
    {
        _currentInput = Vector2.zero;
        _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
    }

    private void GroundMove()
    {
        _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        }
    }

    public void UpdateSpeed(PlayerState playerState)
    {
        if (playerState == null) return;

        switch (playerState.CurrentState)
        {
            case PlayerState.State.Walking:
                _moveSpeed = _walkSpeed;
                break;
            case PlayerState.State.Sprinting:
                _moveSpeed = _sprintSpeed;
                break;
            case PlayerState.State.Crouching:
                _moveSpeed = _crouchSpeed;
                break;

        }
    }
}
