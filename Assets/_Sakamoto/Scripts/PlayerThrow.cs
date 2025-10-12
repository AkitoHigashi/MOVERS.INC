using UnityEngine;

public class PlayerThrow : MonoBehaviour, IStartSetVariables
{
    private LuggageData _luggageData;
    private Transform _cameraForward;
    private float _throwForceForward;
    private float _throwForceUp;
    private float _throwableTime;
    private float _throwTime;
    private bool _isThrowing = false;
    private bool _isCarry = false;

    private void Start()
    {
        _luggageData = GetComponent<LuggageData>();
    }

    private void Update()
    {
        if (_isThrowing)
        {
            _throwTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// 投げる動作を開始する
    /// </summary>
    public void StartThrow()
    {
        _throwTime = 0;
        _isThrowing = true;
    }

    /// <summary>
    /// 投げる動作を終了する
    /// </summary>
    public void StopThrow()
    {
        if (_isCarry && _throwTime >= _throwableTime)
        {
            Throw();
        }
    }

    private void Throw()
    {
        GameObject luggage = _luggageData.Luggage;
        Rigidbody rb = _luggageData.LuggageRb;
        if (luggage == null || rb == null) return;
        luggage.transform.SetParent(null);
        Vector3 forceToAdd = _cameraForward.transform.forward * _throwForceForward
                           + transform.up * _throwForceUp;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
        _luggageData.Luggage = null;
        _luggageData.LuggageRb = null;
        _isThrowing = false;
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _cameraForward = playerData.CameraForward;
        _throwableTime = playerData.ThrowableTime;
        _throwForceForward = playerData.ThrowForceForward;
        _throwForceUp = playerData.ThrowForceUp;
    }

    /// <summary>
    /// 持っているかどうかのフラグをセットする
    /// </summary>
    /// <param name="value"></param>
    public void SetBoolIsCarry(bool value) => _isCarry = value;

    /// <summary>
    /// 投げる動作をしているかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool ReturnIsThrowing() => _isThrowing;
}
