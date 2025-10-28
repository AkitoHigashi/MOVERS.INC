using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ItemBase : MonoBehaviour
{

    [SerializeField] float _power;
    protected Rigidbody _rb;
    protected ItemData _itemdata;
    Inventory _inventory;
    protected Transform _cameraTrans;

    public float Power => _power;
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

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初期設定を行う関数で、必ずbaseも呼び出すこと
    /// </summary>
    protected virtual void Init()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }
        _rb = GetComponent<Rigidbody>();
        _cameraTrans = GameObject.Find("Camera").transform;
        _inventory = FindFirstObjectByType<Inventory>();
    }

    /// <summary>
    /// 手に持たれたときに呼び出す関数
    /// </summary>
    /// <param name="inventoryNum">入力キー</param>
    public void Caught(int inventoryNum)
    {
        _rb.isKinematic = true;
        _inventory.StoreItem(this, inventoryNum);
    }

    /// <summary>
    /// アイテムの効果を発動する関数
    /// </summary>
    public abstract void ItemActivate(LuggageData luggage);
}
