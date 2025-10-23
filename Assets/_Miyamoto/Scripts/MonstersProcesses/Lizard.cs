using UnityEngine;

/// <summary>
/// リザード特有の動きを制御するクラス
/// </summary>
public class Lizard : MonsterBase
{
    [SerializeField, Header("コレクションエリア")]
    private Transform _collectionArea;

    private GameObject _luggage;
    private bool _isCarry;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        if (_isCarry) ThrowLuggage();
        else base.BaseUpdate();

        SetAnimation();
    }
    private void OnEnable()
    {
        base.BaseOnEnable();
    }
    private void OnDisable()
    {
        base.BaseOnDisable();
    }
    private void SetAnimation()
    {
        _animator.SetFloat("WalkSpeed", _navMeshAgent.speed);
    }
    protected override void ProccesToLuggage(Collider collider, float distance)
    {
        Debug.Log("hasSeenがTrueだぞー");
        if (!_hasSeen) FirstSeeing();

        _currentDestination = collider.transform.position;
        if (distance <= _stopDistance)
        {
            Debug.Log("action開始");
            switch (_currentEnemyState)
            {
                case MonsterState.Neutral:
                    CatchLuggage(collider);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 荷物を拾う処理
    /// </summary>
    /// <param name="baggage"></param>
    private void CatchLuggage(Collider luggage)
    {
        if (_isInCollectionArea) return;

        if (!_isCarry)
        {
            Debug.Log("荷物を手に取る");
            var rb = luggage.GetComponent<Rigidbody>();

            _luggage = luggage.gameObject;
            _luggage.transform.position = _facePos.position;
            _luggage.transform.SetParent(this.transform);
            _isCarry = true;

            //目的地から除外
            if (_destinations.Contains(luggage.transform))
            {
                _destinations.Remove(luggage.transform);
            }

            ResetVision();
            CarryLuggage();
            StopAllCoroutines();
            rb.Sleep();
            _coroutine = null;
        }
    }
    /// <summary>
    /// 荷物を運ぶ処理
    /// </summary>
    /// <param name="luggage"></param>
    private void CarryLuggage()
    {
        Debug.Log("荷物を運ぶ");
        _currentDestination = _collectionArea.transform.position;
        _navMeshAgent.SetDestination(_currentDestination);
    }
    /// <summary>
    /// 荷物を下ろす処理
    /// </summary>
    /// <param name="distance"></param>
    private void ThrowLuggage()
    {
        float distance = Vector3.Distance(transform.position, _collectionArea.transform.position);
        if (distance <= _stopDistance)
        {
            Debug.Log("荷物を下す");
            ReleaseLuggage();

            if (_destinations != null && _destinations.Count > 0)
            {
                _currentDestination = _destinations[Random.Range(0, _destinations.Count)].position;
                _navMeshAgent.SetDestination( _currentDestination);
            }
        }
    }
    /// <summary>
    /// 荷物を親子関係から外す
    /// </summary>
    private void ReleaseLuggage()
    {
        if (!_luggage) return;

        Debug.Log("荷物開放");
        _luggage.transform.SetParent(null);
        var rb = _luggage.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.isKinematic = false;
            rb.WakeUp();
        }

        _luggage = null;
        _isCarry = false;
    }

    /// <summary>
    /// 死んだときにもし荷物を持っていたら親子関係を解除
    /// </summary>
    [ContextMenu("LizardDie")]
    protected override void EnemyDie()
    {
        ReleaseLuggage();
        base.EnemyDie();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectionArea"))
            _isInCollectionArea = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectionArea"))
            _isInCollectionArea = false;
    }
}