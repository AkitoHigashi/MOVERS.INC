using UnityEngine;
/// <summary>
/// �G�̏����l���Ǘ�����f�[�^�N���X
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("�G�̏����l")]
    [SerializeField] private int _enemyHp;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private int _enemyFov;
    [SerializeField] private int _enemyPower;
    [SerializeField] private float _enemyAttackRange;

    /// <summary>�G��HP�����l</summary>
    public int EnemyHpData => _enemyHp;
    /// <summary>�G�̈ړ����x�̏����l</summary>
    public float EnemyMoveSpeedData => _enemyMoveSpeed;
    /// <summary>�G�̎���̏����l</summary>
    public int EnemyFoVData => _enemyFov;
    /// <summary>�G�̍U���͂̏����l</summary>
    public int EnemyPowerData => _enemyPower;
    /// <summary>�G�̍U���͈͂̏����l</summary>
    public float EnemyAttackRangeData => _enemyAttackRange;
}