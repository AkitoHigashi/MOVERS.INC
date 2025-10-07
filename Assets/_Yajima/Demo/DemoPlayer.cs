using UnityEngine;

/// <summary>
/// プレイヤー（仮）
/// </summary>
public class DemoPlayer : MonoBehaviour
{
    [SerializeField] ItemBase _item;
    [SerializeField] Inventory _inventory;
    GameObject _go;
    Store _store;
    int _money = 1000;

    private void Start()
    {
        _go = transform.GetChild(0).gameObject;
        _store = FindFirstObjectByType<Store>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _go = _inventory.StoreItem(_go, 0);
            _go?.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _go = _inventory.StoreItem(_go, 1);
            _go?.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _go = _inventory.StoreItem(_go, 2);
            _go?.transform.SetParent(transform);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CatchObject();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _store.PurchaseItem(_go, ref _money);
            Debug.Log(_money);
        }
    }

    void CatchObject()
    {
        _go = transform.GetChild(0).gameObject;
    }

    void UseItem()
    {
        _item.ItemActivate();
    }
}
