using UnityEngine;

/// <summary>
/// 横に薙ぎ払うものにアタッチするクラス
/// </summary>
public class Sweep : ItemBase
{
    [SerializeField] GameObject _sweep;

    protected override void Init()
    {
        base.Init();
        _sweep.SetActive(false);

    }

    [ContextMenu("a")]
    public override void ItemActivate()
    {
        if (!_sweep.activeSelf)
        {
            //プレイヤーの子にする想定
            _sweep.transform.SetParent(_cameraTrans);
            _sweep.transform.position = _cameraTrans.position;
            _sweep.transform.rotation = _cameraTrans.rotation;
            _sweep.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
