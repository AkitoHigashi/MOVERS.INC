using System;
using UnityEngine;

/// <summary>
/// 視野トリガー専用スクリプト
/// </summary>
public class EnemyVision : MonoBehaviour
{
    const string PLAYER = "Player";
    const string LUGGAGE = "Luggage";
    public event Action<Collider> OnFind;
    private Collider _collider;
    private bool _isInSide;
    private void Update()
    {
        if (_isInSide)
        {
            OnFind.Invoke(_collider);
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
