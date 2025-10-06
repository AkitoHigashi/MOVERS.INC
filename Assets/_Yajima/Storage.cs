using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] ItemDataList _itemDataList;

    static Storage _instance;
    Dictionary<ItemData, int> _possessCount = new Dictionary<ItemData, int>();
    public Dictionary<ItemData, int> PossessCount => _possessCount;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            StorageUpdate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void StorageUpdate()
    {
        foreach (var item in _itemDataList.ItemList)
        {
            for (int i = 0; i < _possessCount[item]; i++)
            {
                //アイテムを置く
            }
        }
    }

    public void IteminStorage(ItemData itemData)
    {
        _possessCount[itemData]++;
        StorageUpdate();
    }
}
