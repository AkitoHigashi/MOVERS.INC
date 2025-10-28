
using UnityEngine;
/// <summary>
/// 荷物の大きさを指定する
/// </summary>
public enum LuggageState
{
    small,
    big,
}
/// <summary>
/// 荷物オブジェクト。スコア値を保持し、
/// 一定以上のダメージを受けると破壊される。
/// </summary>
public class Luggage : MonoBehaviour
{
    public int MaxScore;
    public LuggageState State => state;
    [SerializeField] private LuggageState state;
    [SerializeField] private int _score = 100;
    [SerializeField] private LuggageSpeed _luggageSpeed;
    [SerializeField] private float _damageThreshold = 3f; // この速さ未満ならノーダメージ
    [SerializeField] private float _damageScale = 1.0f;   // 速度→ダメージ変換倍率
    protected bool _damage = true;


    private void Start()
    {
        MaxScore = _score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_damage)
        {
            float speed = _luggageSpeed.GetTotalSpeed();
            //   Debug.Log(speed);
            if (speed < _damageThreshold) return;
            int damage = Mathf.RoundToInt((speed - _damageThreshold) * _damageScale);
            int scaledDamage = Mathf.RoundToInt((MaxScore / 100f) * damage);


            _score -= scaledDamage;
            Debug.Log($"衝突: {collision.gameObject.name}, 速度: {speed:F2}, ダメージ: {scaledDamage}, 残りHP: {_score}");

            if (_score <= 0)
            {
                DemolishedLuggage();
            }
        }
        else
        {
            PutLuggage(collision);
        }
    }

    protected virtual void PutLuggage(Collision collision)
    {
        //Empty
    }

    protected virtual void DemolishedLuggage()
    {
        Destroy(gameObject);
    }

    // スコアを取得するプロパティ
    public int Score => _score;



}
