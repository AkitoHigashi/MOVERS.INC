using System.Collections.Generic;
using UnityEngine;
using Item;

public class Storage : MonoBehaviour
{
    [SerializeField, Tooltip("ScriptableObject")] ItemDataList _itemDataList;
    [SerializeField, Tooltip("棚を設定")] ItemShelf[] _itemShelfs;
    /// <summary>アイテムのラベルと棚が対応した辞書</summary>
    Dictionary<ItemLabel, ItemShelf> _itemShelfDic = new Dictionary<ItemLabel, ItemShelf>();

    private void Start()
    {
        MakeDictionary();
        StorageUpdate();
    }

    /// <summary>
    /// ラベルと棚が対応した辞書を作成する関数
    /// </summary>
    void MakeDictionary()
    {
        foreach (var item in _itemShelfs)
        {
            _itemShelfDic.Add(item.ItemType, item);
        }
    }

    /// <summary>
    /// 保管庫の配置を初期化、更新する関数
    /// </summary>
    public void StorageUpdate()
    {
        foreach (var item in _itemDataList.ItemList)
        {
            //まだ買っていないアイテムは飛ばす
            if (!StorageData.PossessCount.ContainsKey(item)) continue;

            for (int i = 0; i < StorageData.PossessCount[item]; i++)
            {
                //アイテムを置く場所が所持数よりも少ないとき
                if (_itemShelfDic[item.ItemLabel].ShelfDatas.Length <= i)
                {
                    Debug.Log($"{item.Item}置く場所がもうありません");
                    break;
                }

                //アイテムを置いているかどうか
                if (!_itemShelfDic[item.ItemLabel].ShelfDatas[i].ItemonShelf)
                {
                    //アイテムを保管庫に並べる
                    var go = Instantiate(item.Item);
                    //アイテムにインベントリに表示する画像を持たせる
                    go.GetComponent<ItemBase>().Sprite = item.Sprite;
                    //設置場所（空のオブジェクト）の子オブジェクトにする
                    go.transform.SetParent(_itemShelfDic[item.ItemLabel].ShelfDatas[i].Position);
                    go.transform.localPosition = Vector3.zero;
                    //棚にアイテムを置いたことを知らせる
                    _itemShelfDic[item.ItemLabel].ShelfDatas[i].PutItem();
                }
            }

            //無駄があっても問題はないが見栄えが悪くなる可能性
            if (_itemShelfDic[item.ItemLabel].ShelfDatas.Length > item.PossessionLimit)
            {
                Debug.Log($"{item.Item}を置く場所に無駄なスペースがあります");
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
    static Dictionary<ItemData, uint> _possessCount = new Dictionary<ItemData, uint>();
    /// <summary>各アイテムの所持数を保持する辞書を受け取るプロパティ</summary>
    public static Dictionary<ItemData, uint> PossessCount => _possessCount;

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
