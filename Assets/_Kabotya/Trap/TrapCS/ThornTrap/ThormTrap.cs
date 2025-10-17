using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ThormTrap : TrapBase
{
    [SerializeField] private TrapRange _trapRange;
    [Tooltip("針がどのくらい上に行くのか（１が最大）")] private float _thormUp = 1f;
    [SerializeField, Tooltip("踏んでから上がるまでの時間")] private float _thormCollTime = 1f;
    [SerializeField, Tooltip("上がる時間")] private float _upDuration = 0.3f;
    [SerializeField, Tooltip("下がる時間")] private float _downDuration = 0.8f;
    [SerializeField, Tooltip("上で止まる時間")] private float _upPauseDuration = 0.2f;
    [SerializeField, Tooltip("下で止まる時間")] private float _downPauseDuration = 0.2f;

    private Tween _thormTween;


    void Awake()
    {
        // 初期化時に適切な容量を設定
        DOTween.SetTweensCapacity(1000, 1000);
    }
    private void Start()
    {
        if (_trapRange == null)
            _trapRange = FindScript.FindInParentOrChildren<TrapRange>(gameObject);
    }

    private void Update()
    {
        TrapCheck();
    }

    private void TrapCheck()
    {
        if (_thormTween.IsActive() && (_thormTween.IsPlaying() || !_thormTween.IsComplete())) return;

        if (!_trapRange._deactivateWhenExit)
        {
            UpThorm();
        }
    }

    private void UpThorm()
    {
        Debug.Log("動いた！！");
        Vector3 startPos = transform.localPosition;
        Vector3 upPos = new Vector3(startPos.x, _thormUp, startPos.z);

        // Sequenceを作成
        Sequence seq = DOTween.Sequence();

        // 上昇 → 停止 → 下降 を順に追加
        seq.AppendInterval(_thormCollTime)
           .Append(transform.DOLocalMove(upPos, _upDuration)
                    .SetEase(Ease.OutCubic)) //上がる
           .AppendInterval(_upPauseDuration)  //上で止まる
           .Append(transform.DOLocalMove(startPos, _downDuration)
                    .SetEase(Ease.InSine))  //下がる
           .AppendInterval(_downPauseDuration)
           .OnComplete(() => Debug.Log("下で止まる"));  //下で止まる

        _thormTween = seq;
    }
}
