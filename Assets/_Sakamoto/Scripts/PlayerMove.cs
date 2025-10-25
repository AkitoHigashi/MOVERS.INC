using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IStartSetVariables
{
    private LuggageData _luggageData;
    private Rigidbody _rb;
    private Transform _cameraForward;
    private bool _currentMuscleItem;
    private float _moveSpeed;
    private float _walkSpeed;
    private float _sprintSpeed;
    private float _crouchSpeed;
    private float _slidingSpeed;
    private float _slidingForce;
    private float _dragSpeedMultiplier;
    private float _bigLuggageSpeedMultiplier;
    private float _startBigLuggageSpeedMultiplier;
    private bool _isSliding = false;
    private bool _isCarrying = false;
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
        if (_isSliding)
        {
            SlidingMove();
        }
        else
        {
            GroundMove();
        }
        if (!_isSliding)
        {
            SpeedControl();
        }
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _currentMuscleItem = playerData.MuscleItem;
        _walkSpeed = playerData.WalkSpeed;
        _sprintSpeed = playerData.SprintSpeed;
        _crouchSpeed = playerData.CrouchSpeed;
        _slidingSpeed = playerData.SlidingSpeed;
        _slidingForce = playerData.SlidingForce;
        _dragSpeedMultiplier = playerData.DragSpeedMultiplier;
        _bigLuggageSpeedMultiplier = playerData.LuggageBigSpeedMultiplier;
        _startBigLuggageSpeedMultiplier = _bigLuggageSpeedMultiplier;
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

    /// <summary>
    /// 地上移動
    /// </summary>
    private void GroundMove()
    {
        _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10, ForceMode.Force);
    }

    /// <summary>
    /// スライディング移動
    /// </summary>
    private void SlidingMove()
    {
        if (_rb.angularVelocity.y > -0.1f)
        {
            _rb.AddForce(_moveDirection.normalized * _slidingForce, ForceMode.Force);
        }
    }

    /// <summary>
    /// 速度制限
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        //Todo:バフ中かどうか判定する
        if (!_currentMuscleItem)
        {
            //荷物を持っているときの速度制限
            if (_luggageData != null && _isCarrying)
            {
                //大きい荷物を持っているときの速度制限
                if (_luggageData.LuggageScript.State == LuggageState.big)
                {
                    if (flatVel.magnitude > _moveSpeed * _bigLuggageSpeedMultiplier)
                    {
                        Vector3 limitedVel = flatVel.normalized * _moveSpeed * _bigLuggageSpeedMultiplier;
                        _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
                    }
                }
            }
            //荷物を持ってないときの速度制限
            else if (flatVel.magnitude > _moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
            }
        }
        else
        {
            //荷物を持っているときの速度制限
            if (_luggageData != null && _isCarrying)
            {
                //大きい荷物を持っているときのバフ速度制限
                if (_luggageData.LuggageScript.State == LuggageState.big)
                {
                    if (flatVel.magnitude > _moveSpeed)
                    {
                        Vector3 limitedVel = flatVel.normalized * _moveSpeed;
                        _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
                    }
                }
            }
            //バフ中の速度制限
            if (flatVel.magnitude > _moveSpeed * _dragSpeedMultiplier)
            {
                Vector3 limitedVel = flatVel.normalized * _moveSpeed * _dragSpeedMultiplier;
                _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    public void IsMuscleItem(bool isMuscleItem)
    {
        _currentMuscleItem = isMuscleItem;
    }

    public void SetBool(bool isSliding, bool isCarrying)
    {
        _isSliding = isSliding;
        _isCarrying = isCarrying;
    }

    /// <summary>
    /// 速度更新
    /// </summary>
    /// <param name="playerState"></param>
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
            case PlayerState.State.Sliding:
                _moveSpeed = _slidingSpeed;
                break;
        }
    }
}
