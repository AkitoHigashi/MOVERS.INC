using System;
using UnityEngine;

/// <summary>
/// 荷物オブジェクト。スコア値を保持し、
/// 一定以上のダメージを受けると破壊される。
/// </summary>
public class Luggage : MonoBehaviour
{
    [SerializeField] private int _score = 100;
    [SerializeField] private LuggageSpeed _luggageSpeed;
    [SerializeField] private float damageThreshold = 3f; // この速さ未満ならノーダメージ
    [SerializeField] private float damageScale = 1.0f;   // 速度→ダメージ変換倍率

    [SerializeField] public int MaxScore;

    private void Start()
    {
        MaxScore = _score;
    }

    private void OnCollisionEnter(Collision collision)
    {

        float speed =  _luggageSpeed.GetTotalSpeed();
     //   Debug.Log(speed);
       if(speed < damageThreshold)return;
       int damage = Mathf.RoundToInt((speed - damageThreshold) * damageScale);
       int scaledDamage = Mathf.RoundToInt((MaxScore / 100f) * damage);
       
  
       _score -=  scaledDamage;
       Debug.Log($"衝突: {collision.gameObject.name}, 速度: {speed:F2}, ダメージ: {scaledDamage}, 残りHP: {_score}");

        if (_score <= 0) Destroy(gameObject);
    }
    

    // スコアを取得するプロパティ
    public int Score => _score;
    


}
