using TMPro;
using UnityEngine;

/// <summary>
/// スコアの管理と表示を行うクラス。
/// スコア加算・減算、テキスト更新、終了フラグ管理を担当する。
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private int _nowScore = 0;
    public static int EndScore { get; set; } = 0;

    private bool _isEnd = false;

    // 現在のスコアを外部に公開
    public int NowScore => _nowScore;


    public void SetScore(int score)
    {

        _nowScore = Mathf.Max(_nowScore + score, 0);

    }
    // スコア表示テキストを更新

    public void SetText()
    {
        Debug.Log(NowScore);
        _tmpText.text = $"Score : {NowScore:D5}";
    }

    // スコア更新を終了状態にする
    public void SetEndScore(bool end) => _isEnd = end;
}
