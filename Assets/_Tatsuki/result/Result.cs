using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text main_text;   // メインスコア表示用テキスト
    [SerializeField] private TMP_Text sub1_text;   // サブスコア①（荷物）
    [SerializeField] private TMP_Text sub2_text;   // サブスコア②（モンスター）
    [SerializeField] private TMP_Text sub3_text;   // サブスコア③（無傷）
    [SerializeField] private TMP_Text total_text;  // トータルスコア表示用テキスト

    [SerializeField] private int bonusScore = 3;   // 各ボーナスのスコア値
    private int score;                             // サブスコアの合計
    private int mainScore;                         // メインスコア（荷物数 × スコア）
    private int luggageNumbers = 1;                // 荷物の数

    private void Start()
    {
        // --- メイン報酬の計算 ---
        int sum = 1;
        mainScore = ScoreManager.EndScore * (sum + luggageNumbers);
      
        main_text.text = $"mainScore {mainScore}";

        // --- 荷物を全て届けたか ---
        bool isDelivered = true;
        if (isDelivered)
        {
            sub1_text.text = $"sub1Score {bonusScore}";
            score += bonusScore;
        }
        else sub1_text.text = "0";

        // --- モンスターを捕まえたか ---
        bool hasCaughtMonster = true;
        if (hasCaughtMonster)
        {
            sub2_text.text = $"sub2Score {bonusScore}";
            score += bonusScore;
        }
        else sub2_text.text = "0";

        // --- 無傷でクリアしたか ---
        bool isUndamaged = true;
        if (isUndamaged)
        {
            sub3_text.text = $"sub3Score {bonusScore}";
            score += bonusScore;
        }
        else sub3_text.text = "0";

        // --- トータルスコアの計算 ---
        total_text.text = $"totalScore {mainScore + score}";
    }
}
