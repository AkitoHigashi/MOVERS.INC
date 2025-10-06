using UnityEngine;

public class PlayerSprint : MonoBehaviour, IStartSetVariables
{
    private float _staminaRecoverySpeed;
    private float _stamina;
    private float _staminaMaxVaue;
    private bool _isSprint = false;

    private void Update()
    {
        StaminaManager();
    }

    /// <summary>
    /// スタミナ管理
    /// </summary>
    private void StaminaManager()
    {
        if (_isSprint)
        {
            _stamina -= Time.deltaTime;
            if (_stamina < 0)
            {
                _isSprint = false;
            }
        }
        else if (!_isSprint)
        {
            if (_stamina <= _staminaMaxVaue)
            {
                _stamina += Time.deltaTime * _staminaRecoverySpeed;
            }
            else return;
        }
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _stamina = playerData.Stamina;
        _staminaMaxVaue = playerData.Stamina;
        _staminaRecoverySpeed = playerData.StaminaRecoverySpeed;
    }

    /// <summary>
    /// ダッシュ開始
    /// </summary>
    public void StartSprint()
    {
        if (_stamina > 0)
        {
            _isSprint = true;
        }
    }

    /// <summary>
    /// ダッシュ終了
    /// </summary>
    public void StopSprint()
    {
        _isSprint = false;
    }

    /// <summary>
    /// ダッシュ中かどうか
    /// </summary>
    /// <returns></returns>
    public bool ReturnIsSprint() => _isSprint;
}
