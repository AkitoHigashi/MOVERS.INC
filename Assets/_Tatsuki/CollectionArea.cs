using System;
using UnityEngine;

/// <summary>
/// 荷物がエリアに入った・出たことを検知し、
/// スコアの加算・減算を行うトリガーエリア。
/// </summary>
public class CollectionArea : MonoBehaviour
{
    // 範囲内に入ったときに通知されるイベント
    public event Action<GameObject> OnEnter;

    // 範囲から出たときに通知されるイベント
    public event Action<GameObject> OnExit;

    // スコア管理クラスへの参照 
    [SerializeField] private ScoreManager scoreManager;

    /// <summary>
    /// 荷物がエリアに入ったときにスコアを加算。
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Luggage"))
        {
            var luggage = other.gameObject.GetComponent<Luggage>();
            scoreManager.SetScore(luggage.Score);
            scoreManager.SetText(scoreManager.NowScore.ToString());
            OnEnter?.Invoke(other.gameObject);
        }
    }

    /// <summary>
    /// 荷物がエリアから出たときにスコアを減算。
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Luggage"))
        {
            var luggage = other.gameObject.GetComponent<Luggage>();
            scoreManager.SetScore(-luggage.Score);
            scoreManager.SetText(scoreManager.NowScore.ToString());
            OnExit?.Invoke(other.gameObject);
        }
    }
}
