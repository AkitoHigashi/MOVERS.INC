using DG.Tweening;
using UnityEngine;

public class FurikoTrap : MonoBehaviour
{

    [Tooltip("下げたらスピードが上がる")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("-1ならループ、その他の整数を入れるとその回数ループ")]
    [SerializeField] private int _loopNumber = -1;

    [Tooltip("何度回転するか")]
    private float _swingAngle = 180f;

    private Tween _furikoTween;

    private void Start()
    {
        RotateFuriko();
    }

    private void Update()
    {
        FurikoCheck();
    }

    private void FurikoCheck()
    {
        if (TrapRange.Instance._deactivateWhenExit)
        {
            // プレイヤーが範囲外にいる場合、トラップを停止
            if (_furikoTween != null && _furikoTween.IsPlaying())
            {
                _furikoTween.Pause();
            }
        }
        else
        {
            // プレイヤーが範囲内にいる場合、トラップを再開
            if (_furikoTween != null && !_furikoTween.IsPlaying())
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
}