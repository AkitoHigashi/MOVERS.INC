using UnityEngine;

/// <summary>
/// 横に薙ぎ払うものにアタッチするクラス
/// </summary>
public class Sweep : ItemBase
{
    [SerializeField] GameObject _sweep;
    private GameObject _cameraPos;//カメラ

    protected override void Init()
    {
        base.Init();
        _sweep.SetActive(false);
        _cameraPos = GameObject.Find("Camera");

    }

    [ContextMenu("a")]
    public override void ItemActivate()
    {
        if (!_sweep.activeSelf)
        {
            //プレイヤーの子にする想定
            _sweep.transform.SetParent(_cameraPos.transform);
            _sweep.transform.position = _cameraPos.transform.position;
            _sweep.transform.rotation = _cameraPos.transform.rotation;
            _sweep.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
