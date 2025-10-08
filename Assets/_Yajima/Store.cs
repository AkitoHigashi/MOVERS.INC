using UnityEngine;

/// <summary>
/// ストアの情報を保持するクラス
/// </summary>
public class Store : MonoBehaviour
{
    [SerializeField] ItemDataList _itemDataList;
    [SerializeField, Tooltip("アイテムを並べる座標")] Transform[] _transforms;
    [SerializeField] Storage _storage;

    /// <summary>仮の会社評価</summary>
    [SerializeField] int _companyReview = 100;

    private void Start()
    {
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
                var go = Instantiate(item.Display);
                go.transform.SetParent(_transforms[positionIndex]);
                go.transform.localPosition = Vector3.zero;
                go.GetComponent<Display>().Data = item;
                positionIndex++;
            }
        }
    }

    /// <summary>
    /// アイテムを購入する関数
    /// </summary>
    /// <param name="item">購入するアイテム</param>
    /// <param name="money">所持金の参照</param>
    /// <return>払う金額の値を返す</return>
    public void PurchaseItem(GameObject item, ref int money)
    {
        var data = item.GetComponent<Display>().Data;
        //お金が足りているかどうか
        if (money >= data.PurchaseCost)
        {
            if (!StorageData.PossessCount.ContainsKey(data))
            {
                //始めて買うとき
                Debug.Log("初購入");
                //所持金を更新
                money -= (int)data.PurchaseCost;
                //保管庫に情報を送る
                StorageData.IteminStorage(data);
                //保管庫の配置を更新
                _storage.StorageUpdate();
            }
            else if (StorageData.PossessCount[data] < data.PossessionLimit)
            {
                Debug.Log("購入");
                //所持金を更新
                money -= (int)data.PurchaseCost;
                //保管庫に情報を送る
                StorageData.IteminStorage(data);
                //保管庫の配置を更新
                _storage.StorageUpdate();
            }
            else
            {
                Debug.Log("これ以上買えません");
            }
        }
        else
        {
            Debug.Log("お金が足りません");
        }
    }
}
