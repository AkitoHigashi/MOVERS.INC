using UnityEngine;
using System.Collections.Generic;
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
    [SerializeField] private Target _target = Target.NotTarget;
    [SerializeField] private GameObject _brokenLuggage;


    InteractBase _interactBase;
    private bool _damage = true;
    private bool _isDead = false;
    public bool IsDead => _isDead;

    // スコアを取得するプロパティ
    public int Score => _score;

    /// <summary>
    /// スコアを更新する関数（段ボール専用）
    /// </summary>
    /// <param name="list"></param>
    public void ChangeScoreInCardboad(List<Luggage> list)
    {
        _score = 0;
        foreach(var luggage in list)
        {
            _score += luggage.Score;
        }
    }

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
                _isDead = true;
                if (_isDead)
                {
                    Instantiate(_brokenLuggage, transform.position, Quaternion.identity);
                    SEManager.SEPlay("BrokenLuggage");
                }
            }
        }
        else
        {
            _interactBase?.PutLuggage(collision);
        }
    }
}
