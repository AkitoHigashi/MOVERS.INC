using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text _main_text; // メインスコア表示用テキスト
    [SerializeField] private TMP_Text _cargo_text; // サブスコア①（荷物）ボーナス
    [SerializeField] private TMP_Text _capture_text; // サブスコア②（モンスター）ボーナス
    [SerializeField] private TMP_Text _undamage_text; // サブスコア③（無傷）ボーナス
    [SerializeField] private TMP_Text _total_text; // トータルスコア表示用テキスト

    [SerializeField] private TMP_Text _cargoStatus_text; //割合
    [SerializeField] private TMP_Text _undamageStatus_text; //割合
    [SerializeField] private TMP_Text _captureStatus_text; //捕獲結果


    // [SerializeField] private int bonusScore = 1000;   
    [SerializeField] private int _cargoBonus = 2000; // 荷物
    [SerializeField] private int _captureBonus = 1000; // 敵や荷物を捕まえた報酬
    [SerializeField] private int _luggageUnDamageBonus = 1000; // 荷物が無傷だった場合の追加ボーナス

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private StatusNotifer _statusNotifer;
    [SerializeField] private Sliders _sliders;
    [SerializeField] private LuggageManager _luggageManager;
    [SerializeField] private InGameTextUIManager _inGameTextUIManager;
    [SerializeField] private LuggageCollector _luggageCollector;
    private int bonusReward = 0; // サブスコアの合計
    private int mainReward = 0; // メインスコア（荷物数 × スコア）
    private int luggageNumbers = 0; // 荷物の数

    private int Quest; //クエストの指定荷物の数
    private int luggageDamage = 0;　//各荷物の合計の割合をいれる

    //荷物品質ボーナスのテスト用の値 
    //ここに各荷物のダメージ割合をいれていく
    //private int lug1 = 50;
    //private int lug2 = 60;
    //private int lug3 = 100;

    //捕獲ボーナステスト用
    //ここに捕まえたかどうかの結果をいれる
    private bool iscapture = false;

    private bool isDelivered = false;
    private bool hasCaughtMonster = false;
    private float _luggageDamageRate = 0f;
    private bool _isUndamaged = false;

    private void OnEnable()
    {
        // --- メイン報酬の計算 ---
        LuggageTargetCheck();
        Quest = _statusNotifer.MaxItem;
        Debug.Log($"NowScore={_scoreManager.NowScore}");
        Debug.Log($"Quest{Quest}");
        Debug.Log($"LuggageNumbers={luggageNumbers}");
        
        mainReward = Mathf.RoundToInt(_scoreManager.NowScore * ((float)luggageNumbers / Quest));
        Debug.Log(mainReward);
        _main_text.text = $" +{mainReward}";

        // --- 荷物を全て届けたか ---
        //仮bool
        if (_sliders.GetLuggage() == 1) isDelivered = true;
        if (isDelivered)
        {
            _cargo_text.text = $"+{_cargoBonus}";
            bonusReward += _cargoBonus;
        }
        else _cargo_text.text = "+0";

        // --- モンスターを捕まえたか ---
        //仮bool

        if (hasCaughtMonster)
        {
            _capture_text.text = $"+{_captureBonus}";
            bonusReward += _captureBonus;
        }
        else _capture_text.text = "+0";

        // --- 無傷でクリアしたか ---
        //仮bool
        LuggageDamageRateCheck();
        if (_isUndamaged)
        {
            _undamage_text.text = $"+{_luggageUnDamageBonus}";
            bonusReward += _luggageUnDamageBonus;
        }
        else _undamage_text.text = "+0";

        // --- トータルスコアの計算 ---
        _total_text.text = $"{mainReward + bonusReward}";


        ///<summary>
        ///ボーナス報酬計算
        ///</summary>

        Debug.Log(_inGameTextUIManager.Count);
        int n = _inGameTextUIManager.Count;
        _cargoStatus_text.text = $"{n}/{Quest}";
        luggageDamage = Mathf.RoundToInt(_luggageDamageRate * 100f);
        _undamageStatus_text.text = $"{luggageDamage}%";

        if (iscapture)
            _captureStatus_text.text = $"成功!!";
        else _captureStatus_text.text = "失敗";
    }

    private void LuggageTargetCheck()
    {
        foreach (var luggage in _luggageCollector.GetLuggage())
        {
            if (luggage.Target == Target.Target) luggageNumbers++;
        }
    }

    private void LuggageDamageRateCheck()
    {
        float totalScore = 0f;
        float totalMaxScore = 0f;
        Debug.Log($"Luggage数: {_luggageManager.GetLuggage().Count}");
        Debug.Log($"Luggage数: {_luggageCollector.GetLuggage().Count}");

        foreach (var luggage in _luggageCollector.GetLuggage())
        {
            if (luggage.Target == Target.Target)
            {
                totalScore += luggage.Score;
                totalMaxScore += luggage.MaxScore;

            }
        }

        if (totalMaxScore <= 0f)
        {
            _luggageDamageRate = 0f;
            _isUndamaged = false;
            return;
        }


        _luggageDamageRate = totalScore / totalMaxScore;

        if (_luggageDamageRate >= 1f) _isUndamaged = true;
        // 無傷判定
        _isUndamaged = Mathf.Approximately(_luggageDamageRate, 1f);
    }
}