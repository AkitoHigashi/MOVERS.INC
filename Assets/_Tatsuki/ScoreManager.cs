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
    [SerializeField] private int _endScore = 0;

    private bool _isEnd = false;

    // 現在のスコアを外部に公開
    public int NowScore => _nowScore;

    // スコアの加算/減算
    public void SetScore(int score)
    {
        if (!_isEnd) _nowScore += score;
    }

    // スコア表示テキストを更新
    public void SetText(string message)
    {
        if (!_isEnd)
            _tmpText.text = message;
    }

    // スコア更新を終了状態にする
    public void SetEndScore(bool end) => _isEnd = end;
}
