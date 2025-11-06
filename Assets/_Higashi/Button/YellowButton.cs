using System.Collections;
using UnityEngine;

/// <summary>
/// ロビーに帰還をするためのボタンクラス
/// </summary>
public class YellowButton : InteractBase
{
    [SerializeField, Header("アニメーションの時間")] private float _animTime;

    private Animator _animator;

    private bool _isProcessing = false;//二重防止
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
        _isProcessing = true;//処理開始

        _animator.SetTrigger("push");

        yield return new WaitForSeconds(_animTime);
        //再生終了後に実行
        //UIManager.Instance.ShowResultUI();
        GlobalParameters.Instance.ModifyDayCount(1);//一日すすめる。理想はロービーに戻るボタンを押した際。
        _isProcessing = false;//処理終了
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
