using UnityEngine;

/// <summary>
/// 荷物オブジェクト。スコア値を保持し、
/// 一定以上のダメージを受けると破壊される。
/// </summary>
public class Luggage : MonoBehaviour
{
    [SerializeField] private int _score = 100;

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突時にスコアを減らす（体力のような扱い）
        _score--;
        if (_score <= 0) Destroy(gameObject);
    }

    // スコアを取得するプロパティ
    public int Score => _score;
}
