using DG.Tweening;
using UnityEngine;

public class Furiko : MonoBehaviour
{
    [SerializeField] private TrapRange _trapRange;
    [Tooltip("下げたらスピードが上がる")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("-1ならループ、その他の整数を入れるとその回数ループ")]
    [SerializeField] private int _loopNumber = -1;
    [Tooltip("何度回転するか")]
    [SerializeField] private float _swingAngle = 180f;
    [SerializeField, Tooltip("SEを再生する間隔（秒）")]
    private float _seInterval = 1f;

    private Tween _furikoTween;
    private float _seTimer = 0f;
    private bool _shouldPlaySE = true;

    private void Start()
    {
        //親オブジェクトと子オブジェクトから探す
        if (_trapRange == null)
        {
            _trapRange = GetComponentInParent<TrapRange>();
            if (_trapRange == null)
            {
                _trapRange = GetComponentInChildren<TrapRange>();
            }
        }
        RotateFuriko();

        // 初回SE再生
        SEManager.SELoopPlay("TrapSwing");
    }

    private void Update()
    {
        TrapCheck();

        // SEを定期的に再生
        if (_shouldPlaySE)
        {
            _seTimer += Time.deltaTime;
            if (_seTimer >= _seInterval)
            {
                SEManager.SELoopPlay("TrapSwing");
                _seTimer = 0f;
            }
        }
    }

    private void TrapCheck()
    {
        if (_trapRange._deactivateWhenExit)
        {
            // プレイヤーが範囲外にいる場合、トラップを停止
            if (_furikoTween.IsPlaying())
            {
                _furikoTween.Pause();
            }
            _shouldPlaySE = false;
            _seTimer = 0f;
        }
        else
        {
            // プレイヤーが範囲内にいる場合、トラップを再開
            if (!_furikoTween.IsPlaying() && !_furikoTween.IsComplete())
            {
                _furikoTween.Play();
            }
            _shouldPlaySE = true;
        }
    }

    private void RotateFuriko()
    {
        _furikoTween = transform.DORotate(new Vector3(0, transform.eulerAngles.y, _swingAngle), _duration)
            .SetLoops(_loopNumber, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        _furikoTween?.Kill();
    }
}