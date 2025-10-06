using UnityEngine;

/// <summary>
/// 視野トリガー専用スクリプト（子オブジェクトにアタッチ）
/// </summary>
public class EnemyVision : MonoBehaviour
{
    private EnemyBase _enemyBase;
    private EnemyMove _enemyMove;
    private SphereCollider _sphereCollider;
    private void Awake()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
        _enemyMove = GetComponentInParent<EnemyMove>();
        _sphereCollider = GetComponentInParent<SphereCollider>();
        _sphereCollider.radius = _enemyBase.EnemyFov;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _enemyMove != null)
        {
            Debug.Log("プレイヤーが入ってきた");
            _enemyMove.FindPlayer(other);
        }
    }
}
