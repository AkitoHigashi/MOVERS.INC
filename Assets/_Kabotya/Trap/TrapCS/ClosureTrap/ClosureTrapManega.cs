using UnityEngine;
using DG.Tweening;

public class VerticalDoor : MonoBehaviour
{
    [SerializeField] private TrapRange _trapRange;
    [SerializeField, Tooltip("上がり降りする時間")]private float _wallUpTime = 3f;
    [SerializeField, Tooltip("上がった位置のY座標")]private float _upPositionY = 5f;
    [SerializeField, Tooltip("下がった位置のY座標")]private float _downPositionY = 0f;
    [SerializeField, Tooltip("クールタイム")]private float _cooldownTime = 1f;
    [Tooltip("残り時間")]private float _cooldownTimer = 0f;
    [SerializeField, Tooltip("閉じるまでの時間")] private float _closedTime = 5f;
    private bool _isUp = false;
    private bool _gravityOnOf = false;
    private Tween _currentTween;

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
    }

    private void Update()
    {
        CoolTime();
        TrapCheck();
    }

    private void CoolTime()
    {

        //クールタイムまで待機
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        //クールタイムが終わっていたら通る
        if (_cooldownTimer <= 0f && _gravityOnOf)
        {
            ToggleDoor();
            // クールタイムをリセット
            _cooldownTimer = _cooldownTime;
        }
    }

    private void TrapCheck()
    {
        if (_trapRange._deactivateWhenExit)
        {
            _gravityOnOf = false;
        }
        else
        {
            _gravityOnOf = true;
        }
    }

    private void ToggleDoor()
    {
        // 前のTweenが残っていたら停止
        _currentTween?.Kill();

        if (_isUp == false)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOMoveY(_downPositionY, _wallUpTime)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => _isUp = false))

               .AppendInterval(_closedTime) // ここで10秒待機

               .Append(transform.DOMoveY(_upPositionY, _wallUpTime)
                    .SetEase(Ease.InCubic)
                    .OnComplete(() => _isUp = true));

            _currentTween = seq;

        }
    }

}
