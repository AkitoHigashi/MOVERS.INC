using UnityEngine;
/// <summary>
/// 敵の初期値を管理するデータクラス
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "Data/MonsterData")]
public class MonsterData : ScriptableObject
{ 
    /// <summary>敵のHP初期値</summary>
    public float MonsterHpData => _monsterHp;
    /// <summary>敵の歩く速度の初期値</summary>
    public float MonsterWalkSpeedData => _monsterWalkSpeed;
    /// <summary>敵の走る速度の初期値</summary>
    public float MonsterRunSpeedData => _monsterRunSpeed;
    /// <summary>敵の視野の初期値</summary>
    public float MonsterFoVData => _monsterFovDistance;
    /// <summary>敵の攻撃力の初期値</summary>
    public float MonsterPowerData => _monsterPower;
    /// <summary>敵の攻撃範囲の初期値</summary>
    public float MonsterAttackRangeData => _monsterAttackRange;
    /// <summary>敵対関係</summary>
    public MonsterState MonsterStateData => _monsterState;
    /// <summary>目的地に到着した時の待機時間</summary>
    public float WaitTime => _waitTime;
    /// <summary>目的地に視点を合わせる速度</summary>
    public float AngularSpeed => _angularSpeed;
    /// <summary>止まるまでの距離</summary>
    public float StopDistance => _stopDistance;
    /// <summary>視野角</summary>
    public float FoV => _fov;
    /// <summary>Fovの拡大倍率</summary>
    public float PatrolFov => _patrolFovDistance;

    /// <summary>
    /// モンスターを捕まえられるか
    /// </summary>
    /// <param name="currentHP"></param>
    /// <returns></returns>
    public bool CanGet(float currentHP)
    {
        return currentHP / _monsterHp <= _hokakuwariai ? true : false;
    }

    [Header("敵の初期値")]
    [SerializeField] private float _monsterHp;
    [SerializeField] private float _monsterWalkSpeed;
    [SerializeField] private float _monsterRunSpeed;
    [SerializeField] private float _monsterFovDistance;
    [SerializeField] private float _monsterPower;
    [SerializeField] private float _monsterAttackRange;

    [Header("敵対関係")]
    [SerializeField] private MonsterState _monsterState;
    [SerializeField, Range(0, 1)] private float _hokakuwariai;

    [Header("移動関係")]
    [SerializeField] private float _waitTime;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _stopDistance = 5f;

    [Header("視点関係")]
    [SerializeField] private float _fov = 60f;
    [SerializeField] private float _patrolFovDistance = 15f;
}
public enum MonsterState
{
    Friendly, // 友好的
    Neutral,  // 中立的
    Hostile   // 敵対的
}