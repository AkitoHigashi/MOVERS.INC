using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IStartSetVariables
{
    public float PlayerHP => _playerHP;
    public float CurrentHP => _currentHP;
    private float _playerHP;
    private float _currentHP;
    public event Action <float> PlayerHealthChanged;
    public void StartSetVariables(PlayerData playerData)
    {
        _playerHP = playerData.Health;
        _currentHP = _playerHP;
     
    }

    private void Dead()
    {
        Debug.Log("Player Dead");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterWeapon"))
        {
            var weapon = other.GetComponent<MonsterWeapon>();
            _currentHP -= weapon.Power;
            PlayerHealthChanged(_currentHP);
            if (_currentHP <= 0)
            {
                Dead();
            }
        }
        else if (other.CompareTag("Trap"))
        {
            var trap=other.GetComponent<TrapBase>();
            _currentHP -= trap.TrapDamage;
            PlayerHealthChanged(_currentHP);
            if (_currentHP <= 0)
            {
                Dead();
            }
        }
    }
    public void TestDamage()
    {
        _currentHP = 10f;
        PlayerHealthChanged(_currentHP);
    }
}
