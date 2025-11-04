using System.Collections;
using UnityEngine;

/// <summary>
/// ロビーに帰還をするためのボタンクラス
/// </summary>
public class YellowButton : InteractBase
{
    [SerializeField, Header("ロードするシーンの名前")] private string _sceneName;
    [SerializeField, Header("アニメーションの時間")] private float _animTime;

    private Animator _animator;

    private bool _isProcessing = false;
    public override void Interact()
    {
        if (_isProcessing) return;

        StartCoroutine(PushEnd());
    }
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private IEnumerator PushEnd()
    {
        _animator.SetTrigger("push");

        yield return new WaitForSeconds(_animTime);
        //再生終了後に実行
        SceneLordManager.Instance.Scnenlode(_sceneName);
    }
    public override void DemolishedLuggage()
    {
        Debug.Log("なにも実装されていません");
    }
    public override void PutLuggage(Collision collision)
    {
        Debug.Log("なにも実装されていません");
    }
}
