using DG.Tweening;
using UnityEngine;

public class ThormTrap : MonoBehaviour
{
    [SerializeField] private TrapRange _trapRange; // 直接参照

    [Tooltip("下げたらスピードが上がる")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("-1ならループ、その他の整数を入れるとその回数ループ")]
    [SerializeField] private int _loopNumber = -1;
    [Tooltip("何度回転するか")]
    [SerializeField] private float _swingAngle = 180f;

    private Tween _furikoTween;

    private void Start()
    {
        // TrapRangeが未設定の場合、親や子から探す
        if (_trapRange == null)
        {
            _trapRange = GetComponentInParent<TrapRange>();
            if (_trapRange == null)
            {
                _trapRange = GetComponentInChildren<TrapRange>();
            }
        }

        if (_trapRange == null)
        {
            Debug.LogError("TrapRangeが見つかりません！Inspectorで設定してください。");
        }

        RotateFuriko();
    }

    private void Update()
    {
        TrapCheck();
    }

    private void TrapCheck()
    {
        // TrapRangeが存在しない場合は何もしない
        if (_trapRange == null || _furikoTween == null)
        {
            return;
        }

        if (_trapRange._deactivateWhenExit)
        {
            // プレイヤーが範囲外にいる場合、トラップを停止
            if (_furikoTween.IsPlaying())
            {
                _furikoTween.Pause();
            }
        }
        else
        {
            // プレイヤーが範囲内にいる場合、トラップを再開
            if (!_furikoTween.IsPlaying() && !_furikoTween.IsComplete())
            {
                _furikoTween.Play();
            }
        }
    }

    private void RotateFuriko()
    {
        _furikoTween = transform.DORotate(new Vector3(0, 0, _swingAngle), _duration)
            .SetLoops(_loopNumber, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        // Tweenをクリーンアップ
        _furikoTween?.Kill();
    }
}