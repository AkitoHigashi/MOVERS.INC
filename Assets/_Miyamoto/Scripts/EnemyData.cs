using UnityEngine;
/// <summary>
/// “G‚Ì‰Šú’l‚ğŠÇ—‚·‚éƒf[ƒ^ƒNƒ‰ƒX
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("“G‚Ì‰Šú’l")]
    [SerializeField] private int _enemyHp;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private int _enemyFov;
    [SerializeField] private int _enemyPower;
    [SerializeField] private float _enemyAttackRange;
    [Header("“G‘ÎŠÖŒW")]
    [SerializeField] private EnemyState _enemyState;

    /// <summary>“G‚ÌHP‰Šú’l</summary>
    public int EnemyHpData => _enemyHp;
    /// <summary>“G‚ÌˆÚ“®‘¬“x‚Ì‰Šú’l</summary>
    public float EnemyMoveSpeedData => _enemyMoveSpeed;
    /// <summary>“G‚Ì‹–ì‚Ì‰Šú’l</summary>
    public int EnemyFoVData => _enemyFov;
    /// <summary>“G‚ÌUŒ‚—Í‚Ì‰Šú’l</summary>
    public int EnemyPowerData => _enemyPower;
    /// <summary>“G‚ÌUŒ‚”ÍˆÍ‚Ì‰Šú’l</summary>
    public float EnemyAttackRangeData => _enemyAttackRange;
    /// <summary>“G‘ÎŠÖŒW</summary>
    public EnemyState EnemyStateData => _enemyState;
}
public enum EnemyState
{
    Friendly, // —FD“I
    Neutral,  // ’†—§“I
    Hostile   // “G‘Î“I
}