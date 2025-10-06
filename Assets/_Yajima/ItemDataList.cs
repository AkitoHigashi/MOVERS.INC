using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Data/ItemData")]
public class ItemDataList : ScriptableObject
{
    [SerializeField] List<ItemData> _itemList;
    public List<ItemData> ItemList => _itemList;
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Tooltip("アイテムのプレハブ")] GameObject _item;
    [SerializeField, Tooltip("購入サンプル")] GameObject _display;
    [SerializeField, Tooltip("底面の中心")] Vector3 _position;
    [SerializeField, Tooltip("購入コスト")] int _purchaseCost;
    [SerializeField, Tooltip("購入するために必要な会社の評価")] int _purchaseCondition;
    [SerializeField, Tooltip("所持上限")] int _possessionLimit;
    [SerializeField] ItemType _type;

    public GameObject Item => _item;
    public GameObject Display => _display;
    public Vector3 Position => _position;
    public int PurchaseCost => _purchaseCost;
    public int PurchaseCondition => _purchaseCondition;
    public int PossessionLimit => _possessionLimit;

    public enum ItemType
    {
        [InspectorName("買い切り")] Endless,
        [InspectorName("消耗品")] Consume
    }
}