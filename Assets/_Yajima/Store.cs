using UnityEngine;

/// <summary>
/// ストアの情報を保持するクラス
/// </summary>
public class Store : MonoBehaviour
{
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField, Tooltip("アイテムを並べる座標")] Transform[] _transforms;

    Storage _storage;

    /// <summary>仮の会社評価</summary>
    [SerializeField] int _companyReview = 100;

    private void Start()
    {
        _storage = FindFirstObjectByType<Storage>();
        StoreSet();
    }

    /// <summary>
    /// ストアの品ぞろえを初期化、更新する関数
    /// </summary>
    void StoreSet()
    {
        int positionIndex = 0;
        foreach (var item in _itemDataList.ItemList)
        {
            //アイテムを購入する条件を達成しているかどうか
            if (item.PurchaseCondition <= _companyReview)
            {
                var go = Instantiate(item.Display, _transforms[positionIndex].position - item.Position, Quaternion.identity);
                go.GetComponent<Display>().Data = item;
                positionIndex++;
            }
        }
    }

    /// <summary>
    /// アイテムを購入する関数
    /// </summary>
    /// <param name="item">購入するアイテム</param>
    /// <param name="money">所持金</param>
    /// <return>払う金額の値を返す</return>
    public int PurchaseItem(GameObject item, int money)
    {
        var data = item.GetComponent<Display>().Data;
        //お金が足りているかどうか
        if (money >= data.PurchaseCost)
        {
            //所持上限まで買っているかどうか
            if (_storage.PossessCount[data] < data.PossessionLimit)
            {
                _storage.IteminStorage(data);
                return data.PurchaseCost;
            }
            else
            {
                Debug.Log("これ以上買えません");
                return 0;
            }
        }
        else
        {
            Debug.Log("お金が足りません");
            return 0;
        }
    }
}
