using UnityEngine;
using System.Collections;

/// <summary>
/// 指定されたボタンを押すと、対象オブジェクトを上下に動かす（落とす→戻す）制御を行うクラス。
/// また、Luggage（荷物）との衝突判定やスコア処理も担当する。
/// </summary>
public class Fall : MonoBehaviour
{
    [SerializeField, Tooltip("落下させたいターゲットオブジェクト")]
    private Transform _target; // 落ちる対象オブジェクト

    [SerializeField, Tooltip("落ちる距離")] private float _fallDistance = 2f; // 落ちる距離
    [SerializeField, Tooltip("落ちるスピード")] private float _fallSpeed = 5f; // 落ちる速度
    [SerializeField, Tooltip("ラゲージコレクター")] private LuggageCollector _collector; // 回収処理を行うクラス

    [SerializeField, Tooltip("ラゲージマネージャー")]
    private LuggageManager _luggageManager; // エリア内の荷物管理クラス

    [SerializeField, Tooltip("スコアマネージャー")] private ScoreManager _scoreManager; // スコア管理クラス
    private InGameTextUIManager _textUImanager;

    private Sliders _sliders;
    private Vector3 _originalPosition; // オブジェクトの初期位置
    private bool _isFalling = false; // 落下中フラグ（二重実行防止）

    /// <summary>
    /// 荷物（Luggage）との衝突を検知し、スコアを減少・オブジェクト破棄を行う。
    /// </summary>q
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Luggage"))
        {
            Debug.Log("Luggage enter");
            var luggage = collision.gameObject.GetComponent<Luggage>();

            // 荷物を破壊
            Destroy(collision.gameObject);
            // 管理リストから削除
            _luggageManager.UnregisterItem(collision.gameObject);

            // スコア減少とUI更新
            _scoreManager.SetScore(-luggage.Score);

            _scoreManager.SetText();
            if (luggage.Target == Target.Target) _sliders.LuggageNum(-1);

            _sliders.LuggageSliderUpdate();

        }
    }

    /// <summary>
    /// 開始時に初期位置を保存し、ボタン押下イベントを登録する。
    /// </summary>
    private void Start()
    {
        _originalPosition = _target.position;
        _sliders = FindAnyObjectByType<Sliders>();
        _textUImanager = FindAnyObjectByType<InGameTextUIManager>();
        _scoreManager.SetText();
    }

    public void FallAndReturnButtonTest()
    {
        StartCoroutine(FallAndReturn());
    }

    /// <summary>
    /// 対象オブジェクトを落とし、一定時間後に元の位置に戻すコルーチン。
    /// 落下中は再度実行されないよう制御する。
    /// </summary>
    public IEnumerator FallAndReturn()
    {
        if (_isFalling) yield break; // 二重実行防止
        _isFalling = true;

        Vector3 targetPosition = _originalPosition + Vector3.down * _fallDistance;
        SEManager.SEPlay("FallObjectSound");

        // ===== 落下フェーズ =====
        while (Vector3.Distance(_target.position, targetPosition) > 0.01f)
        {
            _target.position = Vector3.MoveTowards(_target.position, targetPosition, _fallSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Fall ended");

        // ===== 停止フェーズ =====
        yield return new WaitForSeconds(4f);

        // ===== 上昇フェーズ =====
        while (Vector3.Distance(_target.position, _originalPosition) > 0.01f)
        {
            _target.position = Vector3.MoveTowards(_target.position, _originalPosition, _fallSpeed * Time.deltaTime);
            yield return null;
        }

        _isFalling = false;

        // 荷物を回収（スコア処理完了後に呼び出し）
        _collector.Collect();
        _scoreManager.SetText();
        _sliders.LuggageSliderUpdate();
        _textUImanager.LuggageSetText();
    }
}