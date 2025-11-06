using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の基底クラス
/// </summary>
public abstract class MonsterBase : MonoBehaviour
{
    const string PLAYER = "Player";
    const string LUGGAGE = "Luggage";
    const string ITEM = "Item";
    const string TRAP = "Trap";
    #region プロパティ
    public MonsterData EnemyData => _monsterData;
    public List<Transform> Destinations => _destinations;
    public LayerMask LayerMask => _layerMask;
    public List<string> TargetTags => _targetTags;
    public float WaitTime => _waitTime;
    public float AngularSpeed => _angularSpeed;
    public float StopDistance => _stopDistance;
    public float FoV => _fov;
    public float PatrolFovDistance => _patrolFovDistance;
    public float MonsterHP => _monsterHp;
    public float MonsterWalkSpeed => _monsterWalkSpeed;
    public float MonsterRunSpeed => _monsterRunSpeed;
    public float CurrentSpeed => _currentSpeed;
    public float MonsterFovDistance => _monsterFovDistance;
    public float MonsterPower => _monsterPower;
    public float MonsterAttackRange => _monsterAttackRange;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Vector3 CurrentDestination => _currentDestination;
    public Vector3 LastDesination => _lastDestination;
    public bool HasSeen => _hasSeen;
    #endregion

    [SerializeField, Header("敵のデータ")]
    protected MonsterData _monsterData;
    [SerializeField, Header("顔の場所")]
    protected Transform _facePos;
    [SerializeField, Header("敵が視認できるオブジェクトのレイヤー")]
    protected LayerMask _layerMask;
    [SerializeField, Header("死亡時のエネミーオブジェクト")]
    private GameObject _deadPrefab;
    [SerializeField, Header("ターゲットにするタグ")]
    private List<string> _targetTags;
    [SerializeField, Header("視野の拡大倍率"), Min(1)]
    private float _fovMagnification;

    #region ステータス
    protected float _monsterHp;
    protected float _monsterWalkSpeed;
    protected float _monsterRunSpeed;
    protected float _monsterFovDistance;
    protected float _monsterPower;
    protected float _monsterAttackRange;
    protected float _waitTime;
    protected float _angularSpeed;
    protected float _stopDistance;
    protected float _fov;
    protected float _patrolFovDistance;
    protected float _currentSpeed;
    #endregion

    /// <summary>プレイヤーを見つけたかどうかのフラグ</summary>
    protected bool _hasSeen = false;
    /// <summary>周りを見渡すフラグ<summary>
    protected bool _lookAround = false;
    /// <summary>収集地点に入ったかのフラグ</summary>
    protected bool _isInCollectionArea;
    /// <summary>現在の敵の友好関係<summary>
    protected MonsterState _currentEnemyState;
    /// <summary>目的地のリスト</summary>
    protected List<Transform> _destinations = new List<Transform>();
    /// <summary>現在の目的地</summary>
    protected Vector3 _currentDestination;
    /// <summary>最後に訪れた目的地</summary>
    protected Vector3 _lastDestination;
    /// <summary>徘徊フラグ</summary>
    protected bool _canPatrol = true;

    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected Coroutine _coroutine;
    private Rigidbody _rb;
    private Renderer _renderer;
    private Color _defaultColor;
    /// <summary>
    /// 継承先でAwakeから呼び出す
    /// </summary>
    protected void BaseAwake()
    {
        GetDestination();
        SetParameter();
        VisionGenerator();
    }
    /// <summary>
    /// 継承先でUpdateから呼び出す
    /// </summary>
    protected void BaseUpdate()
    {
        Patrol();
        SetAnimation();
    }
    /// <summary>
    /// 継承先でOnEnableから呼び出す
    /// </summary>
    protected void BaseOnEnable()
    {
        _animator.SetTrigger("Reset");
        _animator.SetBool("LookAround", false);
    }
    /// <summary>
    /// 継承先でOnDisableから呼び出す
    /// </summary>
    protected void BaseOnDisable()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// 敵の初期値を設定する
    /// </summary>
    private void SetParameter()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponentInChildren<Renderer>();
        _defaultColor = _renderer.material.color;

        _monsterHp = _monsterData.MonsterHpData;
        _monsterWalkSpeed = _monsterData.MonsterWalkSpeedData;
        _monsterRunSpeed = _monsterData.MonsterRunSpeedData;
        _currentSpeed = _monsterWalkSpeed;
        _monsterFovDistance = _monsterData.MonsterFoVData;
        _monsterPower = _monsterData.MonsterPowerData;
        _monsterAttackRange = _monsterData.MonsterAttackRangeData;
        _currentEnemyState = _monsterData.MonsterStateData;

        _waitTime = _monsterData.WaitTime;
        _angularSpeed = _monsterData.AngularSpeed;
        _stopDistance = _monsterData.StopDistance;
        _navMeshAgent.stoppingDistance = _stopDistance;
        _fov = _monsterData.FoV;
        _patrolFovDistance = _monsterData.PatrolFov;

        _navMeshAgent.speed = _currentSpeed;
        _currentDestination = _destinations[0].position;
        _lastDestination = _currentDestination;

        _navMeshAgent.angularSpeed = _angularSpeed;
        _rb.isKinematic = true;
    }
    private void SetAnimation()
    {
        _animator.SetBool("LookAround", _lookAround);
    }
    protected virtual void GetDestination()
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destination");
        foreach (GameObject dest in destinations)
        {
            _destinations.Add(dest.transform);
        }
    }
    #region 移動関係
    /// <summary>
    /// 視野を生成
    /// </summary>
    private void VisionGenerator()
    {
        GameObject vision = new GameObject("MonsterVision");

        SphereCollider sphere = vision.AddComponent<SphereCollider>();
        MonsterVision enemyVision = vision.AddComponent<MonsterVision>();
        vision.transform.SetParent(transform);
        vision.transform.localPosition = _facePos.localPosition;

        sphere.radius = _monsterFovDistance;
        sphere.isTrigger = true;
    }
    /// <summary>
    /// 目的地をランダムに徘徊する
    /// </summary>
    private void Patrol()
    {
        //目的地をセットする
        _navMeshAgent.SetDestination(_currentDestination);

        float distance = Vector3.Distance(this.transform.position, _currentDestination);

        if (_canPatrol)
        {
            // アイテムを見つけていない場合で目的地付近にいるなら次の目的地へ
            if (!_navMeshAgent.isStopped && !_hasSeen && distance <= _stopDistance && _coroutine == null)
            {
                Debug.Log("コルーチン開始");
                _coroutine = StartCoroutine(ChangeDestination());
            }
            // アイテム追跡中は徘徊変更処理を止める
            else if (_hasSeen && _coroutine != null)
            {
                Debug.Log("コルーチン停止");
                StopCoroutine(_coroutine);
                _coroutine = null;
                _navMeshAgent.isStopped = false;
                _navMeshAgent.speed = _monsterWalkSpeed;
            }
        }
    }
    /// <summary>
    /// 一時停止して目的地を変更する
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeDestination()
    {
        if (_hasSeen)
        {
            yield break;
        }

        _navMeshAgent.isStopped = true;
        _lookAround = true;
        _navMeshAgent.velocity = Vector3.zero; // 速度をゼロにする
        Debug.Log("一時停止");
        yield return new WaitForSeconds(_waitTime);

        _lastDestination = _currentDestination;
        _currentDestination = _destinations[Random.Range(0, _destinations.Count)].position;
        _navMeshAgent.isStopped = false;
        _lookAround = false;
        _navMeshAgent.speed = _monsterWalkSpeed;

        _coroutine = null;
        Debug.Log("再開");
    }
    /// <summary>
    /// 見失ったら元の目的地に戻る
    /// </summary>
    public void ReturnDestination()
    {
        Debug.Log("見失ったReturnDestination呼び出し");

        _hasSeen = false;
        float _currentDistance = float.MaxValue;
        foreach (var destination in Destinations)
        {
            float distance = Vector3.Distance(this.transform.position, destination.position);
            if (distance < _currentDistance)
            {
                _currentDistance = distance;
                _lastDestination = destination.position;
            }
        }
        _currentDestination = _lastDestination; //元の目的地に戻る
        _navMeshAgent.isStopped = false;
    }
    #endregion

    #region 視野関係
    /// <summary>
    /// オブジェクトが視界に入ったかどうか判定する
    /// </summary>
    /// <param name="collider"></param>
    public void FindObject(Collider collider)
    {
        if (collider == null || _isInCollectionArea) return;

        float distance = Vector3.Distance(this.transform.position, collider.transform.position);

        // 視野角内にいるか判定
        if (IsInSight(collider, out RaycastHit hit, _layerMask))
        {
            OnTargetFind(collider, distance, hit);
        }
        else
        {
            OnTargetLost();
            _navMeshAgent.isStopped = false;
        }
    }
    /// <summary>
    /// 視野角内にいる且つ他のオブジェクトに遮られていないかの判定
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool IsInSight(Collider collider, out RaycastHit hit, LayerMask layers)
    {
        hit = default;

        Vector3 origin = _facePos.position;
        Vector3 toTarget = (collider.transform.position - origin).normalized;
        float targetAngle = Vector3.Angle(transform.forward, toTarget);
        float currentFov = _hasSeen ? _patrolFovDistance : _monsterFovDistance;

        Debug.DrawRay(origin, transform.forward * currentFov, _hasSeen ? Color.red : Color.blue);

        return targetAngle < _fov / 2 &&
                         Physics.Raycast(origin, toTarget, out hit, currentFov, _layerMask) &&
                         hit.collider == collider ? true : false;
    }
    /// <summary>
    ///　見つけた時の判定
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="distance"></param>
    /// <param name="hit"></param>
    private void OnTargetFind(Collider collider, float distance, RaycastHit hit)
    {
        MonsterProcess(collider, distance);
        _coroutine = null;
    }
    /// <summary>
    /// 見失った時に呼び出す
    /// </summary>
    public void OnTargetLost()
    {
        if (_hasSeen)
        {
            Debug.Log("見失ってしもたわいOnTarGetLost呼び出し");
            ResetVision();
            ReturnDestination();
        }
    }
    /// <summary>
    /// 視界内に初めて入った時に呼び出す
    /// </summary>
    protected virtual void FirstSeeing()
    {
        Debug.Log("初めて見えたFirstSeeing呼び出し");
        _navMeshAgent.angularSpeed *= _fovMagnification;
        _fov *= _fovMagnification;
        _hasSeen = true;
    }
    /// <summary>
    /// 見失った時に視野角やスピードを元に戻す
    /// </summary>
    protected void ResetVision()
    {
        Debug.Log("視野角リセットResetVision呼び出し");
        _navMeshAgent.angularSpeed /= _fovMagnification;
        _fov /= _fovMagnification;
        _hasSeen = false;
    }
    /// <summary>
    /// エネミーの処理
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="distance"></param>
    private void MonsterProcess(Collider collider, float distance)
    {
        if (collider == null) return;

        if (collider.CompareTag(PLAYER))
        {
            ProcessToPlayer(collider, distance);
        }
        else if (collider.CompareTag(LUGGAGE))
        {
            ProcessToLuggage(collider, distance);
        }
    }
    /// <summary>
    /// プレイヤーに何かしらの行動を行う
    /// </summary>
    /// <param name="player"></param>
    /// <param name="distance"></param>
    protected virtual void ProcessToPlayer(Collider player, float distance) { }
    /// <summary>
    /// 荷物に何かしらの行動を行う
    /// </summary>
    /// <param name="luggage"></param>
    /// <param name="distance"></param>
    protected virtual void ProcessToLuggage(Collider luggage, float distance) { }
    #endregion

    #region 状態関係
    protected virtual void BaseOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TRAP))
        {
            var trap = collision.gameObject.GetComponent<TrapBase>();
            TakeDamage(trap.TrapDamage);
        }
        else if (collision.gameObject.CompareTag(ITEM))
        {
            var item = collision.gameObject.GetComponent<ItemBase>();
            TakeDamage(item.Power);
        }
    }
    /// <summary>
    /// 攻撃を食らった時の計算
    /// </summary>
    /// <param name="damage"></param>
    private void TakeDamage(float damage)
    {
        Debug.Log($"{this.name}が{damage}ダメージ食らった");
        _monsterHp -= damage;
        Damaged();
        if (_monsterHp <= 0)
        {
            MonsterDead();
        }
    }
    [ContextMenu("Damaged")]
    private void Damaged()
    {
        _renderer.material.DOColor(Color.red, 0.5f).OnComplete(() =>
        {
            _renderer.material.DOColor(_defaultColor, 0.5f);
        });
        SEManager.SEPlay("MonsterDamaged");
    }
    /// <summary>
    /// HPが0以下になったら死亡
    /// </summary>
    [ContextMenu("Die")]
    protected virtual void MonsterDead()
    {
        _navMeshAgent.speed = 0;
        Quaternion quaternion = transform.rotation;
        this.gameObject.SetActive(false);
        Instantiate(_deadPrefab, this.transform.position, quaternion);
    }
    /// <summary>
    /// 友好関係を変える
    /// </summary>
    /// <param name="enemyState"></param>
    protected void ChangeState(MonsterState enemyState)
    {
        _currentEnemyState = enemyState;
    }
    #endregion
}
