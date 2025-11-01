using UnityEngine;
/// <summary>
/// 荷物の大きさを指定する
/// </summary>
public enum LuggageState
{
    small,
    big,
}

public enum Target
{
    Target,
    NotTarget
}

/// <summary>
/// 荷物オブジェクト。スコア値を保持し、
/// 一定以上のダメージを受けると破壊される。
/// </summary>
public class Luggage : MonoBehaviour
{
    public int MaxScore;
    public LuggageState State => state;
    public Target Target { get { return _target; } set { _target = value; } }
    private LuggageSpeed _luggageSpeed;
    [SerializeField] private LuggageState state;
    [SerializeField] private int _score = 100;
    [SerializeField] private float _damageThreshold = 3f; // この速さ未満ならノーダメージ
    [SerializeField] private float _damageScale = 1.0f;   // 速度→ダメージ変換倍率
    [SerializeField] private float _fixedDamage = 10f;
    Target _target = Target.NotTarget;

    InteractBase _interactBase;
    private bool _damage = true;

    // スコアを取得するプロパティ
    public int Score => _score;

    public void NoDamage()
    {
        _damage = false;
    }
    public void TakeDamage()
    {
        _damage = true;
    }


    private void Awake()
    {
        _interactBase = GetComponent<InteractBase>();
        _luggageSpeed = GetComponent<LuggageSpeed>();
        MaxScore = _score;
        _damage = true;

    }

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_damage)
        {
            float speed = _luggageSpeed.GetTotalSpeed();

            float ratioDamage = (MaxScore / 100f) * speed;
            if (speed < _damageThreshold) return;
            // int damage = Mathf.RoundToInt((speed - _damageThreshold) * _damageScale);
            int scaledDamage = Mathf.RoundToInt(_fixedDamage + ratioDamage * _damageScale);


            _score -= scaledDamage;
            Debug.Log($"衝突: {collision.gameObject.name}, 速度: {speed:F2}, ダメージ: {scaledDamage}, 残りHP: {_score}");

            if (_score <= 0)
            {
                _interactBase?.DemolishedLuggage();
            }
        }
        else
        {
            _interactBase?.PutLuggage(collision);
        }
    }
}
