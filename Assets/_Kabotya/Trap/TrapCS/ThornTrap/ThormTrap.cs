using DG.Tweening;
using UnityEngine;

public class ThormTrap : MonoBehaviour
{
    [SerializeField] private TrapRange _trapRange;
    [SerializeField, Tooltip("下げたらスピードが上がる")]private float _duration = 1f;
    [SerializeField,Tooltip("-1ならループ、その他の整数を入れるとその回数ループ")] private int _loopNumber = -1;
    [Tooltip("針がどのくらい上に行くのか（１が最大）")]private float _thormUp = 1f;
    [SerializeField,Tooltip("上がる時間")] private float _upDuration = 0.3f;
    [SerializeField, Tooltip("下がる時間")] private float _downDuration = 0.8f;
    [SerializeField,Tooltip("上で止まる時間")] private float _upPauseDuration = 0.2f;
    [SerializeField, Tooltip("下で止まる時間")] private float _downPauseDuration = 0.2f;

    private Tween thormTween;

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
    }

    private void Update()
    {
        TrapCheck();
    }

    private void TrapCheck()
    {
        if (_trapRange._deactivateWhenExit)
        {
            // プレイヤーが範囲外にいる場合、トラップを停止
            if (thormTween.IsPlaying())
            {
                thormTween.Pause();
            }
        }
        else
        {
            // プレイヤーが範囲内にいる場合、トラップを再開
            if (!thormTween.IsPlaying() && !thormTween.IsComplete())
            {
                thormTween.Play();
            }
        }
    }

    private void RotateFuriko()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 upPos = new Vector3(startPos.x, _thormUp, startPos.z);

        // Sequenceを作成
        Sequence seq = DOTween.Sequence();

        // 上昇 → 停止 → 下降 を順に追加
        seq.Append(transform.DOLocalMove(upPos, _upDuration)
                    .SetEase(Ease.OutCubic)) //上がる
           .AppendInterval(_upPauseDuration)  //上で止まる
           .Append(transform.DOLocalMove(startPos, _downDuration)
                    .SetEase(Ease.InSine))  //下がる
           .AppendInterval(_downPauseDuration)  //下で止まる
           .SetLoops(_loopNumber, LoopType.Restart); //ループ

        thormTween = seq;
    }
}
