using System.Linq;
using UnityEngine;

public class Bat : MonsterBase
{
    [SerializeField, Header("この距離より近い目的地はスキップ")]
    private float _skipDestinationDistance;
    private bool _isFind;
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();
        SetAnimation();
        if (_isFind)
        {
            _navMeshAgent.speed = _monsterRunSpeed; 
        }
        float dis = Vector3.Distance(this.transform.position, _currentDestination);
        if (dis <= _stopDistance)
        {
            _canPatrol = true;
            _navMeshAgent.speed = _monsterWalkSpeed;
        }
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
    private void SetAnimation()
    {
        _animator.SetBool("Run", _isFind);
    }
    protected override void ProcessToPlayer(Collider player, float distance)
    {
        if (!HasSeen) FirstSeeing();

        RunAwayFromPlayer(player);
        _canPatrol = false;
    }
    /// <summary>
    /// プレイヤーから逃げる
    /// </summary>
    private void RunAwayFromPlayer(Collider player)
    {
        if (_isFind) return;

        Debug.Log("逃げるんだよぉ～");
        Transform farthestDes = transform;
        Vector3 toPlayerDir = (transform.position - player.transform.position).normalized;

        foreach (Transform des in _destinations.OrderBy(x => Vector3.Distance(x.position, transform.position)))
        {
            Vector3 toDesDir = (des.position - transform.position).normalized;

            float dot = Vector3.Dot(toPlayerDir, toDesDir);
            float dis = Vector3.Distance(transform.position, des.transform.position);
            //プレイヤー方向と逆の一番近い徘徊ポイントを目的地にセットする
            if (dot <= 0 && dis >= _skipDestinationDistance)
            {
                _currentDestination = des.position;
                _isFind = true;
                _hasSeen = true;
                return;
            }
            //目的地が見つからなかった場合一番遠い目的地をセットする
            farthestDes = des;
        }

        transform.LookAt(farthestDes);
        _currentDestination = farthestDes.position;
    }
}
