using UnityEngine;

/// <summary>
/// ロビーに帰還をするためのボタンクラス
/// </summary>
public class YellowButton : InteractBase
{
    [SerializeField, Header("ロードするシーンの名前")] private string _sceneName;
    private Animator _animator;
    public override void Interact()
    {
        _animator.SetTrigger("push");
        SceneLordManager.Instance.Scnenlode(_sceneName);
    }
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
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
