using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// インベントリのクラス
/// </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryData> _inventoryList;

    /// <summary>
    /// アイテムの格納、取り出しを行う関数
    /// </summary>
    /// <param name="item">格納するアイテム</param>
    /// <param name="inventoryNum">格納するインベントリのインデックス</param>
    /// <returns>
    /// 格納しようとしているものの種類によって、格納されていたアイテム、null、格納しようとしているもののいずれか
    /// </returns>
    public GameObject StoreItem(GameObject item, int inventoryNum)
    {
        if (!item)
        {
            //何も持っていなければnullでインベントリの関数を実行
            Debug.Log($"インベントリ{inventoryNum + 1}から取り出し");
            return _inventoryList[inventoryNum].StoreItem(item);
        }
        else if (item.tag == "Item")
        {
            //アイテムを持っていたらそのアイテムでインベントリの関数を実行
            Debug.Log($"インベントリ{inventoryNum + 1}に格納");
            item.transform.SetParent(this.transform);
            return _inventoryList[inventoryNum].StoreItem(item);
        }
        else
        {
            //アイテム以外を格納しようとしていたらアイテム取り出し
            //ここは後で変更する可能性ある箇所
            //持っているものをその場に置く場合
            item.transform.SetParent(null);
            Debug.Log("アイテム以外を格納することはできません");
            return _inventoryList[inventoryNum].StoreItem(null);
        }
    }

    /// <summary>
    /// インベントリのデータのクラス
    /// </summary>
    [System.Serializable]
    public class InventoryData
    {
        [SerializeField] Image _image;
        Queue<GameObject> _inventory = new Queue<GameObject>();

        /// <summary>
        /// アイテムの格納、取り出しを行う関数
        /// </summary>
        /// <param name="item">格納するアイテム</param>
        /// <returns>取り出したアイテムかnull</returns>
        public GameObject StoreItem(GameObject item)
        {
            //アイテムを取り出す処理
            GameObject go = null;
            if (_inventory.Count > 0)
            {
                go = _inventory.Dequeue();
                go.SetActive(true);
                _image.sprite = null;
            }

            //アイテムをしまう処理
            if (item)
            {
                _inventory.Enqueue(item);
                item.SetActive(false);
                //アイテムのスプライトを設定する
                _image.sprite = item.GetComponent<ItemBase>().ItemData.Sprite;
            }

            return go;
        }
    }
}
