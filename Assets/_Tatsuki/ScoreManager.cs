using TMPro;
using UnityEngine;

/// <summary>
/// スコアの管理と表示を行うクラス。
/// スコア加算・減算、テキスト更新、終了フラグ管理を担当する。
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] InGameTextUIManager _textUIManager;
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
        _textUIManager.SetText(_nowScore);
    }

    // スコア更新を終了状態にする
    public void SetEndScore(bool end) => _isEnd = end;
}
