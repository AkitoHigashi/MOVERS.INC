using UnityEngine;

/// <summary>
/// 武器オブジェクトにベースの攻撃力を持たせる
/// </summary>
public class MonsterWeapon : MonoBehaviour
{
    public float Power => _power;

    [SerializeField, Tooltip("この武器を持っているモンスターのベース")]
    private MonsterBase _enemyBase;

    private float _power;

    private void Awake()
    {
        _power = _enemyBase.MonsterPower;
    }
}
