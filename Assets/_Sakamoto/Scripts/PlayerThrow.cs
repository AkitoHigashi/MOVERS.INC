using UnityEngine;

public class PlayerThrow : MonoBehaviour, IStartSetVariables
{
    private Transform _cameraForward;
    private Transform _luggagePosition;
    private float _throwForceForward;
    private float _throwForceUp;
    private float _throwableTime;
    private float _throwTime;
    private bool _isThrowing = false;
    private bool _isCarry = false;

    private void Update()
    {
        if (_isThrowing)
        {
            _throwTime += Time.deltaTime;
        }
    }

    public void StartThrow()
    {
        _throwTime = 0;
        _isThrowing = true;
    }

    public void StopThrow()
    {
        if (_isCarry && _throwTime >= _throwableTime)
        {
            Throw();
        }
    }

    private void Throw()
    {

    }

    public void StartSetVariables(PlayerData playerData)
    {
        _cameraForward = playerData.CameraForward;
        _luggagePosition = playerData.LuggagePosition;
        _throwableTime = playerData.ThrowableTime;
        _throwForceForward = playerData.ThrowForceForward;
        _throwForceUp = playerData.ThrowForceUp;
    }

    public void SetBoolIsCarry(bool isCarry) => _isCarry = isCarry;
    public bool IsThrowing => _isThrowing;
}
