using UnityEngine;

/// <summary>
/// 視野トリガー専用スクリプト
/// </summary>
public class MonsterVision : MonoBehaviour
{
    private MonsterBase _monsterBase;
    private Collider _collider;
    private bool _isInSide;
    private void Start()
    {
        _monsterBase = GetComponentInParent<MonsterBase>();
    }
    private void Update()
    {
        if (_isInSide)
        {
            _monsterBase.FindObject(_collider);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in _monsterBase.TargetTags)
        {
            if (other.CompareTag(tag))
            {
                Debug.Log("範囲内になにか入ってきた");
                _collider = other;
                _isInSide = true;
                break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == _collider)
        {
            _collider = null;
        }
    }
}