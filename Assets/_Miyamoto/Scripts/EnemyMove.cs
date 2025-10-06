using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の動きを制御するクラス
/// </summary>
public class EnemyMove : EnemyBase
{
    [SerializeField, Header("目的地のリスト")]
    private List<Transform> _destinations = new List<Transform>();
    [SerializeField, Header("目的地に到着した時の待機時間")]
    private float _waitTime;
    [SerializeField, Header("目的地に視点を合わせる速度")]
    private float _angularSpeed;
    [SerializeField, Header("止まるまでの距離")]
    private float _stopDistance;
    [SerializeField, Header("視野角")]
    private float _angle;

    /// <summary>現在の目的地</summary>
    private Vector3 _currentDestination;
    /// <summary>最後に訪れた目的地</summary>
    private Vector3 _lastDestination;
    /// <summary>プレイヤーを見つけたかどうかのフラグ</summary>
    private bool hasSeenPlayer = false;

    protected override void Awake()
    {
        base.Awake();
        _currentDestination = _destinations[0].position;
        _lastDestination = _currentDestination;
        _navMeshAgent.angularSpeed = _angularSpeed;
    }
    private void Update()
    {
        //目的地をセットする
        _navMeshAgent.SetDestination(_currentDestination);

        float distance = Vector3.Distance(this.transform.position, _currentDestination);

        // プレイヤーを見つけていない場合で目的地近くにいるなら次の目的地へ
        if (!_navMeshAgent.isStopped && !IsFind && distance <= 1)
        {
            StartCoroutine(ChangeDestination());
        }
        // プレイヤー追跡中は巡回変更処理を止める
        else if (IsFind)
        {
            StopCoroutine(ChangeDestination());
        }
    }
    /// <summary>
    /// プレイヤーが視界に入ったかどうか判定して追尾する
    /// </summary>
    /// <param name="playerCollider"></param>
    public void FindPlayer(Collider playerCollider)
    {
        Vector3 origin = transform.position;
        Vector3 toPlayer = (playerCollider.transform.position - origin).normalized;
        float targetAngle = Vector3.Angle(transform.forward, toPlayer);
        float distance = Vector3.Distance(this.transform.position, playerCollider.transform.position);

        bool raycastHitPlayer = false;//raycastがプレイヤーに当たったかどうか

        // 視野角内にいるか判定
        if (targetAngle < _angle / 2)
        {
            RaycastHit hit;
            // Raycastで障害物の有無をチェックする
            if (Physics.Raycast(origin, toPlayer, out hit, _enemyFov) && hit.collider == playerCollider)
            {
                raycastHitPlayer = true;
            }
        }

        if (raycastHitPlayer)
        {
            Debug.Log("プレイヤーを発見");
            hasSeenPlayer = true;
            IsFind = true;
            _currentDestination = playerCollider.transform.position;//プレイヤーを標的にセット
            _navMeshAgent.isStopped = false;
            if (distance < 5)
            {
                //ここに攻撃をするかどうか判定する処理
                Debug.Log("攻撃");
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            // プレイヤーが視界から外れた時に見失う処理
            if (hasSeenPlayer)
            {
                Debug.Log("プレイヤーを視界から見失った");
                hasSeenPlayer = false;
                IsFind = false;
                _currentDestination = _lastDestination; //元の目的地に戻る
                _navMeshAgent.isStopped = false;
            }
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

    //敵の視界を可視化する関数(必要に応じてコメントアウトして)
    //private void OnDrawGizmos()
    //{
    //    if (_navMeshAgent == null) return;

    //    Vector3 origin = transform.position;
    //    Vector3 forward = transform.forward;
    //    float viewAngle = _angle; // 視野角
    //    int segments = 20;

    //    // 色を設定
    //    Gizmos.color = IsFind ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);

    //    // 扇形を三角形で塗りつぶす
    //    for (int i = 0; i < segments; i++)
    //    {
    //        float angle1 = -_angle / 2 + (viewAngle * i / segments);
    //        float angle2 = -_angle / 2 + (viewAngle * (i + 1) / segments);

    //        Vector3 dir1 = Quaternion.Euler(0, angle1, 0) * forward * _enemyFov;
    //        Vector3 dir2 = Quaternion.Euler(0, angle2, 0) * forward * _enemyFov;

    //        // 三角形を描画
    //        Vector3[] vertices = new Vector3[] { origin, origin + dir1, origin + dir2 };

    //        // Gizmosで三角形を塗りつぶし
    //        DrawTriangle(vertices[0], vertices[1], vertices[2]);
    //    }
    //}
    //private void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    //{
    //    Gizmos.DrawLine(p1, p2);
    //    Gizmos.DrawLine(p2, p3);
    //    Gizmos.DrawLine(p3, p1);

    //    // 少し高さを変えて重ねることで塗りつぶしのように見せる
    //    for (float t = 0; t <= 1; t += 0.1f)
    //    {
    //        Vector3 a = Vector3.Lerp(p1, p2, t);
    //        Vector3 b = Vector3.Lerp(p1, p3, t);
    //        Gizmos.DrawLine(a, b);
    //    }
    //}
}