using DG.Tweening;
using UnityEngine;

public class SpawnPositionController : MonoBehaviour
{
    private MeshRenderer _renderer;
    [SerializeField] private Material _origMaterial;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    /// <summary>
    /// モンスター生成処理
    /// 一応変色して拡大縮小しているだけ
    /// </summary>
    /// <param name="monster"></param>
    /// <returns></returns>
    public bool SpawnMonster(TempMonsterData monster)
    {
        // TODO Instantiateなどを使うはず。ここではデバッグ的な挙動にする

        _renderer.material = monster.Material;
        transform.DOScale(Vector3.one * 8, 1f)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => _renderer.material = _origMaterial);
        return true;
    }

    /// <summary>
    /// 「選定したけど、プレイヤー近いからやめた」という意味を表す
    /// デバッグ用
    /// </summary>
    public void SkipSpawn()
    {
        transform.DOScale(Vector3.one * 8, 0.5f)
            .SetLoops(2, LoopType.Yoyo);
    }
}
