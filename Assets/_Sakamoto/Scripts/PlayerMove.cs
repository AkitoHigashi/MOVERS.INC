using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IStartSetVariables
{
    private Rigidbody _rb;
    private Transform _cameraForward;
    private float _moveSpeed;
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
        AddSpeed();
        SpeedController();
    }


    public void StartSetVariables(PlayerData playerData)
    {
        _moveSpeed = playerData.MoveSpeed;
        _cameraForward = playerData.CameraForward;
    }

    public void Move(Vector2 input,PlayerData playerData)
    {
        _currentInput = input;
        _cameraForward= playerData.CameraForward;
    }

    public void Stop()
    {
        _currentInput = Vector2.zero;
        _rb.linearVelocity = Vector3.zero;
    }

    private void AddSpeed()
    {
        _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedController()
    {
        if (_rb.linearVelocity.magnitude > _moveSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * _moveSpeed;
        }
    }
}
