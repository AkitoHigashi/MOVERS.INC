using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// エリア内の荷物をまとめて回収する処理を担当。
/// 回収した数をカウントし、スコア更新を停止する。
/// </summary>
public class LuggageCollector : MonoBehaviour
{
    [SerializeField] private LuggageManager _luggageManager;
    [SerializeField] private ScoreManager _scoreManager;

    public void Collect()
    {
        int collectedCount = 0;
        List<GameObject> toRemove = new List<GameObject>();

        // エリア内の全荷物を回収
        foreach (var item in _luggageManager.GetItemInArea())
        {
            collectedCount++;
            toRemove.Add(item);
            Destroy(item);
        }

        // 登録解除とスコア停止
        foreach (var item in toRemove)
            _luggageManager.UnregisterItem(item);

        _scoreManager.SetEndScore(true);
        Debug.Log($"Collected {collectedCount} items!");
    }
}
