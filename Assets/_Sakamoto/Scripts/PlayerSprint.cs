<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905

public class PlayerSprint : MonoBehaviour, IStartSetVariables
{
    private float _staminaRecoverySpeed;
    private float _stamina;
<<<<<<< HEAD
    private float _staminaMaxVaue;
=======
    private float _staminaMaxValue;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    private bool _isSprint = false;

    private void Update()
    {
        StaminaManager();
    }

    /// <summary>
<<<<<<< HEAD
    /// �X�^�~�i�Ǘ�
=======
    /// スタミナ管理
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
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
<<<<<<< HEAD
            if (_stamina <= _staminaMaxVaue)
=======
            if (_stamina <= _staminaMaxValue)
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
            {
                _stamina += Time.deltaTime * _staminaRecoverySpeed;
            }
            else return;
        }
    }

    public void StartSetVariables(PlayerData playerData)
    {
        _stamina = playerData.Stamina;
<<<<<<< HEAD
        _staminaMaxVaue = playerData.Stamina;
=======
        _staminaMaxValue = playerData.Stamina;
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
        _staminaRecoverySpeed = playerData.StaminaRecoverySpeed;
    }

    /// <summary>
<<<<<<< HEAD
    /// �_�b�V���J�n
=======
    /// ダッシュ開始
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    /// </summary>
    public void StartSprint()
    {
        if (_stamina > 0)
        {
            _isSprint = true;
        }
    }

    /// <summary>
<<<<<<< HEAD
    /// �_�b�V���I��
=======
    /// ダッシュ終了
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    /// </summary>
    public void StopSprint()
    {
        _isSprint = false;
    }

    /// <summary>
<<<<<<< HEAD
    /// �_�b�V�������ǂ���
=======
    /// ダッシュ中かどうか
>>>>>>> 1f4ad52ea75e7d3628b5e82c8569a960909f3905
    /// </summary>
    /// <returns></returns>
    public bool ReturnIsSprint() => _isSprint;
}
