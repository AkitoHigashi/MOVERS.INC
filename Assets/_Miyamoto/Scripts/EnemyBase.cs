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
    protected Vector3 _currentDestination;
    protected Vector3 _lastDestination;
    protected bool isFind = false;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
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
