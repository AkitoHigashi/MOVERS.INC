using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̊��N���X
/// </summary>
public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;

    //�X�e�[�^�X
    protected int _enemyHp;
    protected float _enemyMoveSpeed;
    protected int _enemyFov;
    protected int _enemyPower;
    protected float _enemyAttackRange;

    protected NavMeshAgent _navMeshAgent;
    private bool _isFind = false;

    public float EnemyFov => _enemyFov;
    public bool IsFind
    {
        get { return _isFind; }
        set { _isFind = value; }
    }

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetParameter();
    }
    /// <summary>
    /// �G�̏����l��ݒ肷��
    /// </summary>
    private void SetParameter()
    {
        _enemyHp = _enemyData.EnemyHpData;
        _enemyMoveSpeed = _enemyData.EnemyMoveSpeedData;
        _enemyFov = _enemyData.EnemyFoVData;
        _enemyPower = _enemyData.EnemyPowerData;
        _enemyAttackRange = _enemyData.EnemyAttackRangeData;
        _navMeshAgent.speed = _enemyMoveSpeed;
    }
}
