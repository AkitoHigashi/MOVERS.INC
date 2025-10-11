using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// デーモンキング特有の動きを制御するクラス
/// </summary>
public class DemonKing : EnemyBase
{
    [SerializeField, Header("攻撃のクールタイム(秒)")]
    private float _coolTime = 2f;

    private float timer;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();
        SetAnimation();
        timer += Time.deltaTime;
    }
    private void OnEnable()
    {
        base.BaseOnEnable();
    }
    private void OnDisable()
    {
        base.BaseOnDisable();

        _animator.SetTrigger("Reset");
        _animator.SetBool("Run", false);
        _animator.SetBool("LookAround", false);
    }
    private void SetAnimation()
    {
        _animator.SetBool("LookAround", _lookAround);
        _animator.SetBool("Run", HasSeen);
    }
    protected override void ProccesToPlayer(Collider collider, float distance)
    {

        if (!_hasSeen) FirstSeeing();
       
        _navMeshAgent.speed = _enemyRunSpeed;
        _currentDestination = collider.transform.position;

        if (CanAction(distance))
        {
            switch (_currentEnemyState)
            {
                case EnemyState.Hostile:
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
    private bool CanAction(float distance)
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
        _navMeshAgent.speed = _enemyRunSpeed;
    }
}