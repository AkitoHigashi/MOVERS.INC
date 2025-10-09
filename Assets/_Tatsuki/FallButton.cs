using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 指定されたボタンを押すと、対象オブジェクトを上下に動かす（落とす→戻す）制御を行うクラス。
/// また、Luggage（荷物）との衝突判定やスコア処理も担当する。
/// </summary>
public class FallButton : MonoBehaviour
{
    [SerializeField] private Button button;               // 押すボタン
    [SerializeField] private Transform target;            // 落ちる対象オブジェクト
    [SerializeField] private float fallDistance = 2f;     // 落ちる距離
    [SerializeField] private float fallSpeed = 5f;        // 落ちる速度
    [SerializeField] private LuggageCollector collector;  // 回収処理を行うクラス
    [SerializeField] private LuggageManager luggageManager; // エリア内の荷物管理クラス
    [SerializeField] private ScoreManager scoreManager;   // スコア管理クラス

    private Vector3 originalPosition; // オブジェクトの初期位置
    private bool isFalling = false;   // 落下中フラグ（二重実行防止）

    /// <summary>
    /// 荷物（Luggage）との衝突を検知し、スコアを減少・オブジェクト破棄を行う。
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Luggage"))
        {
            var luggage = collision.gameObject.GetComponent<Luggage>();

            // 管理リストから削除
            luggageManager.UnregisterItem(collision.gameObject);

            // 荷物を破壊
            Destroy(collision.gameObject);

            // スコア減少とUI更新
            scoreManager.SetScore(-luggage.Score);
         
            scoreManager.SetText(scoreManager.NowScore.ToString());
        }
    }

    /// <summary>
    /// 開始時に初期位置を保存し、ボタン押下イベントを登録する。
    /// </summary>
    private void Start()
    {
        originalPosition = target.position;

        // ボタン押下時に「落下→戻る」動作を開始
        button.onClick.AddListener(() => StartCoroutine(FallAndReturn()));
    }

    /// <summary>
    /// 対象オブジェクトを落とし、一定時間後に元の位置に戻すコルーチン。
    /// 落下中は再度実行されないよう制御する。
    /// </summary>
    private IEnumerator FallAndReturn()
    {
        if (isFalling) yield break; // 二重実行防止
        isFalling = true;

        Vector3 targetPosition = originalPosition + Vector3.down * fallDistance;

        // ===== 落下フェーズ =====
        while (Vector3.Distance(target.position, targetPosition) > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position, targetPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Fall ended");

        // ===== 停止フェーズ =====
        yield return new WaitForSeconds(1f);

        // ===== 上昇フェーズ =====
        while (Vector3.Distance(target.position, originalPosition) > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position, originalPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        isFalling = false;

        // 荷物を回収（スコア処理完了後に呼び出し）
        collector.Collect();
    }
}
