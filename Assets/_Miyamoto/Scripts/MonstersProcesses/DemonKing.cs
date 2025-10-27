using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// デーモンキング特有の動きを制御するクラス
/// </summary>
public class DemonKing : MonsterBase
{
    [SerializeField, Header("攻撃のクールタイム(秒)")]
    private float _coolTime = 2f;

    private float _timer;
    private bool _isAttacking;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();
        SetAnimation();
        _timer += Time.deltaTime;
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

        if (distance < _stopDistance && _timer >= _coolTime)
        {
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
        if (!player) return;

        //アニメーションとか攻撃を走らせる
        Debug.Log($"{this.name}の攻撃");

        _isAttacking = true;
        Vector3 velocity = this.transform.position;
        velocity.x = 0;
        velocity.z = 0;
        _navMeshAgent.velocity = velocity;
        _animator.SetTrigger("Attack");
        _timer = 0;
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