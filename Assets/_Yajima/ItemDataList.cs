using System.Collections.Generic;
using UnityEngine;
using Item;

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
    [SerializeField, Tooltip("購入コスト")] uint _purchaseCost;
    [SerializeField, Tooltip("購入するために必要な会社の評価")] uint _purchaseCondition;
    [SerializeField, Tooltip("所持上限")] uint _possessionLimit;
    [SerializeField, Tooltip("アイテムのラベル")] ItemLabel _itemLabel;

    public GameObject Item => _item;
    public GameObject Display => _display;
    public uint PurchaseCost => _purchaseCost;
    public uint PurchaseCondition => _purchaseCondition;
    public uint PossessionLimit => _possessionLimit;
    public ItemLabel ItemLabel => _itemLabel;
}

namespace Item
{
    /// <summary>アイテムを区別するためのラベル</summary>
    public enum ItemLabel
    {
        //これをコピーしてラベルを追加
        //[InspectorName("")]
        [InspectorName("回復薬")] Heal,
        [InspectorName("モンスターボール")] Ball
    }
}