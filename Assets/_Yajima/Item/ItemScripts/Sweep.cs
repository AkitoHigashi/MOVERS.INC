using UnityEngine;

/// <summary>
/// 横に薙ぎ払うものにアタッチするクラス
/// </summary>
[RequireComponent(typeof(Animator))]
public class Sweep : ItemBase
{
    [SerializeField] string _animationName;
    [SerializeField] float _power;
    Animator _anim;
    /// <summary>2度振り禁止フラグ</summary>
    bool _isPlaying;

    protected override void Init()
    {
        base.Init();
        _anim = GetComponent<Animator>();
    }

    public override void ItemActivate()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
            _anim.Play(_animationName);
        }
    }

    /// <summary>
    /// アニメーションイベントで呼び出す
    /// </summary>
    public void ActivateEnd()
    {
        _isPlaying = false;
    }
}
