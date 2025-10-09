using UnityEngine;
using Item;

/// <summary>
/// 棚そのものにアタッチするスクリプト
/// </summary>
public class ItemShelf : MonoBehaviour
{
    [SerializeField, Tooltip("子オブジェクトにした空のオブジェクトをアイテムが置かれる順番に設定")] ShelfData[] _shelfDatas;
    [SerializeField, Tooltip("アイテムのラベル")] ItemLabel _itemType;
    public ShelfData[] ShelfDatas => _shelfDatas;
    public ItemLabel ItemType => _itemType;

    [System.Serializable]
    public class ShelfData
    {
        [SerializeField] Transform _position;
        bool _itemonShelf;

        public Transform Position => _position;
        public bool ItemonShelf => _itemonShelf;

        /// <summary>
        /// アイテムを動かしたときに呼び出す関数
        /// </summary>
        public void MoveItem()
        {
            _itemonShelf = false;
        }

        /// <summary>
        /// アイテムを置いたときに呼び出す関数
        /// </summary>
        public void PutItem()
        {
            _itemonShelf = true;
        }
    }
}
