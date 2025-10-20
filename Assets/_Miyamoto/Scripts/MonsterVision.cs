using System;
using UnityEngine;

/// <summary>
/// 視野トリガー専用スクリプト
/// </summary>
public class MonsterVision : MonoBehaviour
{
    const string PLAYER = "Player";
    const string LUGGAGE = "Luggage";
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
        if (other.CompareTag(PLAYER) || other.CompareTag(LUGGAGE))
        {
            Debug.Log("範囲内になにか入ってきた");
            _collider = other;
            _isInSide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER) || other.CompareTag(LUGGAGE))
        {
            _isInSide = false;
            _collider = null;
        }
    }
}
