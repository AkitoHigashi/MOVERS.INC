using UnityEngine;

/// <summary>
/// 視野トリガー専用スクリプト（子オブジェクトにアタッチ）
/// </summary>
public class EnemyVision : MonoBehaviour
{
    const string PLAYER = "Player";
    const string LUGGAGE = "Luggage";
    private EnemyBase _enemyBase;
    private Collider _collider;
    private bool _isTrigger;
    private void Start()
    {
        _enemyBase = GetComponentInParent<EnemyBase>();
    }
    private void Update()
    {
        if (_isTrigger)
        {
            _enemyBase.FindObject(_collider);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER) || CompareTag(LUGGAGE))
        {
            Debug.Log("範囲内になにか入ってきた");
            _collider = other;
            _isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || CompareTag("Baggage"))
        {
            _enemyBase.ReturnDestination();
            _isTrigger = false;
        }
    }
}
