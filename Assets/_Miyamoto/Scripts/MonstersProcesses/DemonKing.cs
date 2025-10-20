using UnityEngine;

/// <summary>
/// デーモンキング特有の動きを制御するクラス
/// </summary>
public class DemonKing : MonsterBase
{
    [SerializeField, Header("攻撃のクールタイム(秒)")]
    private float _coolTime = 2f;

    private float timer;
    private bool _attack;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();
        SetAnimationBool();
        timer += Time.deltaTime;
    }
    private void OnEnable()
    {
        base.BaseOnEnable();
    }
    private void OnDisable()
    {
        base.BaseOnDisable();
        _animator.SetBool("Run", false);
    }
    private void SetAnimationBool()
    {
        _animator.SetBool("Run", HasSeen);
        _animator.SetBool("Idle", timer >= _coolTime);
        _animator.SetBool("Attack", _attack);
    }
    protected override void ProccesToPlayer(Collider collider, float distance)
    {
        if (!_hasSeen) FirstSeeing();
       
        _navMeshAgent.speed = _monsterRunSpeed;
        _currentDestination = collider.transform.position;

        if (CanAttack(distance))
        {
            switch (_currentEnemyState)
            {
                case MonsterState.Hostile:
                    Attack(collider);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// アクションを起こせるか判定
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private bool CanAttack(float distance)
    {
        if (distance < _stopDistance && timer >= _coolTime)
        {
            // X軸とZ軸をゼロにし、Y軸は保持して回転はできるように
            Vector3 velocity = _navMeshAgent.velocity;
            velocity.x = 0;
            velocity.z = 0;
            _navMeshAgent.velocity = velocity;
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// プレイヤーに攻撃
    /// </summary>
    /// <param name="player"></param>
    private void Attack(Collider player)
    {
        if (player == null) return;

        //アニメーションとか攻撃を走らせる
        Debug.Log($"{this.name}の攻撃");
        _animator.SetTrigger("Attack");
        timer = 0;
    }
    /// <summary>
    /// 速度アップ追加
    /// </summary>
    protected override void FirstSeeing()
    {
        base.FirstSeeing();
        _navMeshAgent.speed = _monsterRunSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectionArea"))
            ReturnDestination();
    }
}