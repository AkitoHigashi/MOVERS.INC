using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text main_text;   // メインスコア表示用テキスト
    [SerializeField] private TMP_Text cargo_text;   // サブスコア①（荷物）ボーナス
    [SerializeField] private TMP_Text capture_text;   // サブスコア②（モンスター）ボーナス
    [SerializeField] private TMP_Text undamage_text;   // サブスコア③（無傷）ボーナス
    [SerializeField] private TMP_Text total_text;  // トータルスコア表示用テキスト

    [SerializeField] private TMP_Text cargoStatus_text; //割合
    [SerializeField] private TMP_Text undamageStatus_text;//割合
    [SerializeField] private TMP_Text captureStatus_text;//捕獲結果


   // [SerializeField] private int bonusScore = 1000;   
    [SerializeField] private int cargoBonus = 2000;     // 荷物
    [SerializeField] private int captureBonus = 1000;      // 敵や荷物を捕まえた報酬
    [SerializeField] private int luggageUnDamageBonus = 1000;       // 荷物が無傷だった場合の追加ボーナス

    private int bonusReward = 0;                             // サブスコアの合計
    private int mainReward = 0;                     // メインスコア（荷物数 × スコア）
    private int luggageNumbers = 1;                // 荷物の数

    private int Quest = 10;   //クエストの指定荷物の数
    private int luggageDamage = 0;　//各荷物の合計の割合をいれる

    //荷物品質ボーナスのテスト用の値 
    //ここに各荷物のダメージ割合をいれていく
    private int lug1 = 50;
    private int lug2 = 60;
    private int lug3 = 100;

    //捕獲ボーナステスト用
    //ここに捕まえたかどうかの結果をいれる
    private bool iscapture = true;

    private void Start()
    {
        // --- メイン報酬の計算 ---

      
        
        mainReward = Mathf.RoundToInt(ScoreManager.EndScore * ((float)luggageNumbers /Quest));
        main_text.text = $" +{mainReward}";

        // --- 荷物を全て届けたか ---
        //仮bool
        bool isDelivered = true;
        if (isDelivered)
        {
            cargo_text.text = $"+{cargoBonus}";
            bonusReward += cargoBonus;
        }
        else cargo_text.text = "+0";

        // --- モンスターを捕まえたか ---
        //仮bool
        bool hasCaughtMonster = true;
        if (hasCaughtMonster)
        {
            capture_text.text = $"+{captureBonus}";
            bonusReward += captureBonus;
        }
        else capture_text.text = "+0";

        // --- 無傷でクリアしたか ---
        //仮bool
        bool isUndamaged = true;
        if (isUndamaged)
        {
            undamage_text.text = $"+{luggageUnDamageBonus}";
            bonusReward += luggageUnDamageBonus;
        }
        else undamage_text.text = "+0";

        // --- トータルスコアの計算 ---
        total_text.text = $"{mainReward + bonusReward}";


        ///<summary>
        ///ボーナス報酬計算
        ///</summary>

        
        cargoStatus_text.text = $"{luggageNumbers}/{Quest}";

        luggageDamage = Mathf.RoundToInt((float)(lug1 + lug2 + lug3) / 3);
        undamageStatus_text.text = $"{luggageDamage}%";

        if(iscapture)
        captureStatus_text.text = $"成功!!";
        else captureStatus_text.text = "失敗";

    }
}
