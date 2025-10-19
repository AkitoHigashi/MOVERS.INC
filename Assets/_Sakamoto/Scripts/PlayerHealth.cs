using UnityEngine;

public class PlayerHealth : MonoBehaviour, IStartSetVariables
{
    private float _playerHP;
    private float _currentHP;
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
            if (_currentHP <= 0)
            {
                Dead();
            }
        }
    }
}
