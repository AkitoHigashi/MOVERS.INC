using UnityEngine;

/// <summary>
/// リザード特有の動きを制御するクラス
/// </summary>
public class Lizard : EnemyBase
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
                case EnemyState.Neutral:
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
        Debug.Log("ThrowLuggage");
        float distance = Vector3.Distance(transform.position, _collectionArea.transform.position);
        if (distance <= _stopDistance)
        {
            Debug.Log("親子関係解除");
            _currentDestination = _destinations[Random.Range(0, _destinations.Count)].position;
            Collider collider = _luggage.GetComponent<Collider>();
            var rb = collider.GetComponent<Rigidbody>();

            _luggage.transform.SetParent(null);
            rb.WakeUp();
            _isCarry = false;
        }
    }

    /// <summary>
    /// 死んだときにもし荷物を持っていたら親子関係を解除
    /// </summary>
    protected override void EnemyDie()
    {
        base.EnemyDie();
        if (_luggage.transform.parent == this) _luggage.transform.SetParent(null);
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