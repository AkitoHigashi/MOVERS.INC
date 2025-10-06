using UnityEngine;
/// <summary>
/// 敵の初期値を管理するデータクラス
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("敵の初期値")]
    [SerializeField] private int _enemyHp;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private int _enemyFov;
    [SerializeField] private int _enemyPower;
    [SerializeField] private float _enemyAttackRange;

    /// <summary>敵のHP初期値</summary>
    public int EnemyHpData => _enemyHp;
    /// <summary>敵の移動速度の初期値</summary>
    public float EnemyMoveSpeedData => _enemyMoveSpeed;
    /// <summary>敵の視野の初期値</summary>
    public int EnemyFoVData => _enemyFov;
    /// <summary>敵の攻撃力の初期値</summary>
    public int EnemyPowerData => _enemyPower;
    /// <summary>敵の攻撃範囲の初期値</summary>
    public float EnemyAttackRangeData => _enemyAttackRange;
}