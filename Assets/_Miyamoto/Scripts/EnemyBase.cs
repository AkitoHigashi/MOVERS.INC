using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    const string PLAYER = "Player";
    const string LUGGAGE = "Luggage";
    public List<Transform> Destinations => _destinations;
    public float WaitTime => _waitTime;
    public float AngularSpeed => _angularSpeed;
    public float StopDistance => _stopDistance;
    public float Angle => _angle;
    public float Magnification => _magnification;
    public float EnemyHP => _enemyHp;
    public float EnemyMoveSpeed => _enemyMoveSpeed;
    public float EnemyFov => _enemyFov;
    public float EnemyPower => _enemyPower;
    public float EnemyAttackRange => _enemyAttackRange;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Vector3 CurrentDestination => _currentDestination;
    public Vector3 LastDesination => _lastDestination;
    public bool HasSeen => _hasSeen;

    [SerializeField, Header("敵のデータ")]
    protected EnemyData _enemyData;
    [SerializeField, Header("目的地のリスト")]
    protected List<Transform> _destinations = new List<Transform>();
    [SerializeField, Header("目的地に到着した時の待機時間")]
    protected float _waitTime;
    [SerializeField, Header("目的地に視点を合わせる速度")]
    protected float _angularSpeed;
    [SerializeField, Header("止まるまでの距離")]
    protected float _stopDistance = 5f;
    [SerializeField, Header("視野角")]
    protected float _angle;
    [SerializeField, Header("オブジェクトを見つけた時のFovの拡大倍率")]
    protected float _magnification;

    //ステータス
    protected float _enemyHp;
    protected float _enemyMoveSpeed;
    protected float _enemyFov;
    protected float _enemyPower;
    protected float _enemyAttackRange;

    /// <summary>プレイヤーを見つけたかどうかのフラグ</summary>
    protected bool _hasSeen = false;
    protected EnemyState _currentEnemyState;
    /// <summary>現在の目的地</summary>
    protected Vector3 _currentDestination;
    /// <summary>最後に訪れた目的地</summary>
    protected Vector3 _lastDestination;
    protected NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetParameter();
        VisionGenerator();
    }
    private void Update()
    {
        Patrol();
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
        _currentEnemyState = _enemyData.EnemyStateData;
        _navMeshAgent.speed = _enemyMoveSpeed;
        _currentDestination = _destinations[0].position;
        _lastDestination = _currentDestination;
        _navMeshAgent.angularSpeed = _angularSpeed;
    }
    private void VisionGenerator()
    {
        GameObject vision = new GameObject("EnemyVision");

        SphereCollider sphere = vision.AddComponent<SphereCollider>();
        EnemyVision enemyVision = vision.AddComponent<EnemyVision>();
        vision.transform.SetParent(transform);
        vision.transform.localPosition = Vector3.zero;

        sphere.radius = _enemyFov;
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

        // プレイヤーを見つけていない場合で目的地付近にいるなら次の目的地へ
        if (!_navMeshAgent.isStopped && !_hasSeen && distance <= _stopDistance)
        {
            StartCoroutine(ChangeDestination());
        }
        // プレイヤー追跡中は巡回変更処理を止める
        else if (_hasSeen)
        {
            StopCoroutine(ChangeDestination());
        }
    }
    /// <summary>
    /// 一時停止して目的地を変更する
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeDestination()
    {
        _navMeshAgent.isStopped = true;
        Debug.Log("一時停止");
        yield return new WaitForSeconds(_waitTime);

        _lastDestination = _currentDestination;
        _currentDestination = _destinations[Random.Range(0, _destinations.Count)].position;
        _navMeshAgent.isStopped = false;
        Debug.Log("再開");
    }
    /// <summary>
    /// 見失ったら元の目的地に戻る
    /// </summary>
    public void ReturnDestination()
    {
        Debug.Log("見失った");
        _hasSeen = false;
        _currentDestination = _lastDestination; //元の目的地に戻る
        _navMeshAgent.isStopped = false;
    }
    /// <summary>
    /// オブジェクトが視界に入ったかどうか判定する
    /// </summary>
    /// <param name="collider"></param>
    public void FindObject(Collider collider)
    {
        Vector3 origin = transform.position;
        Vector3 toPlayer = (collider.transform.position - origin).normalized;
        float targetAngle = Vector3.Angle(transform.forward, toPlayer);
        float distance = Vector3.Distance(this.transform.position, collider.transform.position);
        float currentFov = _enemyFov;

        // 視野角内にいるか判定
        if (targetAngle < _angle / 2)
        {
            RaycastHit hit;
            // Raycastで障害物の有無をチェックする
            if (Physics.Raycast(origin, toPlayer, out hit, _enemyFov) && hit.collider == collider)
            {
                _enemyFov = _magnification;
                _hasSeen = true;
                EnemyProcces(collider, distance);
            }
            else if (_hasSeen)
            {
                _enemyFov = currentFov;
                return;
            }
        }
    }
    /// <summary>
    /// エネミーの処理
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="distance"></param>
    private void EnemyProcces(Collider collider, float distance)
    {
        if (collider.CompareTag(PLAYER))
        {
            ProccesToPlayer(collider, distance);
        }
        else if (collider.CompareTag(LUGGAGE))
        {
            ProccesToLuggage(collider, distance);
        }
    }
    /// <summary>
    /// プレイヤーに何かしらの行動を行う
    /// </summary>
    /// <param name="player"></param>
    /// <param name="distance"></param>
    protected virtual void ProccesToPlayer(Collider player, float distance) { }
    /// <summary>
    /// 荷物に何かしらの行動を行う
    /// </summary>
    /// <param name="baggage"></param>
    /// <param name="distance"></param>
    protected virtual void ProccesToLuggage(Collider baggage, float distance) { }

    //敵の視界を可視化する関数(必要に応じてコメントアウトして<3)
    private void OnDrawGizmos()
    {
        if (_navMeshAgent == null) return;

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        float viewAngle = _angle; // 視野角
        int segments = 20;

        // 色を設定
        Gizmos.color = _hasSeen ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);

        // 扇形を三角形で塗りつぶす
        for (int i = 0; i < segments; i++)
        {
            float angle1 = -_angle / 2 + (viewAngle * i / segments);
            float angle2 = -_angle / 2 + (viewAngle * (i + 1) / segments);

            Vector3 dir1 = Quaternion.Euler(0, angle1, 0) * forward * _enemyFov;
            Vector3 dir2 = Quaternion.Euler(0, angle2, 0) * forward * _enemyFov;

            // 三角形を描画
            Vector3[] vertices = new Vector3[] { origin, origin + dir1, origin + dir2 };

            // Gizmosで三角形を塗りつぶし
            DrawTriangle(vertices[0], vertices[1], vertices[2]);
        }
    }
    private void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p1);

        // 少し高さを変えて重ねることで塗りつぶしのように見せる
        for (float t = 0; t <= 1; t += 0.1f)
        {
            Vector3 a = Vector3.Lerp(p1, p2, t);
            Vector3 b = Vector3.Lerp(p1, p3, t);
            Gizmos.DrawLine(a, b);
        }
    }
}
