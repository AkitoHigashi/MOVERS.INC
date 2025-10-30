using System.Collections;
using UnityEngine;

/// <summary>
/// デーモンキング特有の動きを制御するクラス
/// </summary>
public class DemonKing : MonsterBase
{
    [SerializeField, Header("攻撃のクールタイム(秒)")]
    private float _coolTime = 2f;
    [SerializeField, Header("視界から見失っても追跡できる時間(秒)")]
    private float _chaseTime;

    private float _timer;
    private bool _isAttacking;
    private float _lostSightTimer;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();
        SetAnimation();
        _timer += Time.deltaTime;

        // 視界を失っても一定時間は追跡を続ける
        if (!_hasSeen && _lostSightTimer < _chaseTime)
        {
            _lostSightTimer += Time.deltaTime;
            _navMeshAgent.SetDestination(_currentDestination);
        }
    }
    private void OnEnable()
    {
        base.BaseOnEnable();
        _isAttacking = false;
    }
    private void OnDisable()
    {
        base.BaseOnDisable();
        _animator.SetBool("Run", false);
        _isAttacking = false;
    }
    private void SetAnimation()
    {
        _animator.SetBool("Wait", _timer >= _coolTime);
        _animator.SetBool("Run", HasSeen); 
    }
    protected override void ProcessToPlayer(Collider collider, float distance)
    {
        if (!_hasSeen)
        {
            FirstSeeing();
            _lostSightTimer = 0f; // プレイヤーを再度見つけたらリセット
        }

        _navMeshAgent.speed = _monsterRunSpeed;
        _currentDestination = collider.transform.position;

        if (CanAttack(distance))
        {
            if (_currentEnemyState == MonsterState.Hostile)
                StartCoroutine(AttackRoutine(collider));
        }
        else
        {
            _isAttacking = false;
        }
    }
    /// <summary>
    /// アクションを起こせるか判定
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private bool CanAttack(float distance)
    {
        if(_isAttacking) return false;
        return (distance < _stopDistance && _timer >= _coolTime);
    }
    /// <summary>
    /// 攻撃コルーチン
    /// </summary>
    private IEnumerator AttackRoutine(Collider player)
    {
        if (!player) yield break;
        if (_isAttacking) yield break;

        _isAttacking = true;
        _navMeshAgent.isStopped = true;

        Debug.Log($"{this.name}の攻撃");
        _animator.SetTrigger("Attack");
        _timer = 0;

        yield return new WaitForSeconds(1.2f); // 攻撃アニメーション時間

        _isAttacking = false;
        _navMeshAgent.isStopped = false;
    }
    /// <summary>
    /// 速度アップ追加
    /// </summary>
    protected override void FirstSeeing()
    {
        base.FirstSeeing();
        _navMeshAgent.speed = _monsterWalkSpeed ;
    }
    private void OnCollisionEnter(Collision collision)
    {
        BaseOnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("CollectionArea"))
        {
            Debug.Log("コレクションエリアに入った");
            _isAttacking = false;
            ReturnDestination();
        }
    }
}