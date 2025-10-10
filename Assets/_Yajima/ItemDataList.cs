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
    [Header("クエスト内で使うデータ")]
    [SerializeField, Tooltip("アイテムのプレハブ")] GameObject _item;
    [SerializeField, Tooltip("インベントリに表示する画像")] Sprite _sprite;
    [Header("購入に関するデータ")]
    [SerializeField, Tooltip("購入サンプル")] GameObject _display;
    [SerializeField, Tooltip("購入コスト")] uint _purchaseCost;
    [SerializeField, Tooltip("購入するために必要な会社の評価")] uint _purchaseCondition;
    [SerializeField, Tooltip("所持上限")] uint _possessionLimit;
    [SerializeField, Tooltip("アイテムのラベル")] ItemLabel _itemLabel;

    public GameObject Item => _item;
    public Sprite Sprite => _sprite;
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
        [InspectorName("剣")] Sword,
        [InspectorName("棒")] Rod,
        [InspectorName("ツールキット")]　Tool_Kit,
        [InspectorName("段ボール")] Cardboard,
        [InspectorName("クッションマット")] Cushion,
        [InspectorName("ローブ")] Robe,
        [InspectorName("回復ポーション")] Potion,
        [InspectorName("梯子")] Ladder,
        [InspectorName("モンスタースフィア")] Sphere
    }
}