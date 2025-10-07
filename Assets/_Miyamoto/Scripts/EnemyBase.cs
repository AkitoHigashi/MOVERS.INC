using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の基底クラス
/// </summary>
public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyData _enemyData;

    //ステータス
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
    /// 敵の初期値を設定する
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
