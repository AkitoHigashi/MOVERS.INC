using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] Sprite _sprite;
    protected Rigidbody _rb;
    protected ItemData _itemdata;
    /// <summary>
    /// アイテムのデータのやり取りをするプロパティ
    /// </summary>
    public ItemData ItemData
    {
        get
        {
            return _itemdata;
        }
        set
        {
            if (_itemdata == null)
            {
                _itemdata = value;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期設定を行う関数で、必ずbaseも呼び出すこと
    /// </summary>
    protected virtual void Init()
    {
        _rb = GetComponent<Rigidbody>();
        if (tag != "Item")
        {
            tag = "Item";
        }
    }

    /// <summary>
    /// アイテムの効果を発動する関数
    /// </summary>
    public abstract void ItemActivate();
}
