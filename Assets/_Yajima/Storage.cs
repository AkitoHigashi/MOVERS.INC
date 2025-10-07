using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] ItemDataList _itemDataList;

    private void Start()
    {
        StorageUpdate();
    }

    /// <summary>
    /// 保管庫の配置を初期化、更新する関数
    /// </summary>
    public void StorageUpdate()
    {
        foreach (var item in _itemDataList.ItemList)
        {
            if (!StorageData.PossessCount.ContainsKey(item)) continue;
            for (int i = 0; i < StorageData.PossessCount[item]; i++)
            {
                //アイテムを置く
            }
        }
    }
}

/// <summary>
/// 保管庫の情報を保持する
/// </summary>
public static class StorageData
{
    /// <summary>各アイテムの所持数を保持する辞書</summary>
    static Dictionary<ItemData, int> _possessCount = new Dictionary<ItemData, int>();
    /// <summary>各アイテムの所持数を保持する辞書を受け取るプロパティ</summary>
    public static Dictionary<ItemData, int> PossessCount => _possessCount;

    /// <summary>
    /// 購入したアイテムの所持数を1増やす関数
    /// </summary>
    /// <param name="itemData">購入したアイテム</param>
    public static void IteminStorage(ItemData itemData)
    {
        if (!_possessCount.ContainsKey(itemData))
        {
            _possessCount.Add(itemData, 0);
        }
        _possessCount[itemData]++;
    }
}
