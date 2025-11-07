using UnityEngine;

/// <summary>
/// ストアに並べるオブジェクトにアタッチするスクリプト
/// </summary>
public class Display : InteractBase
{
    private Store _store;
    private GlobalParameters _globalParameter;
    /// <summary>このアイテムのデータ</summary>
    ItemData _data;

    /// <summary>
    /// アイテムの購入に関する情報をやりとりするプロパティ
    /// </summary>
    public ItemData Data
    {
        get
        {
            return _data;
        }
        set
        {
            if (_data == null)
            {
                _data = value;
            }
        }
    }

    public override void DemolishedLuggage()
    {
        //Empty
    }

    public override void Interact()
    {
        _globalParameter.ModifyMoney(-_store.PurchaseItem(this.gameObject, _globalParameter.Money));
        Debug.Log($"所持金計算をした:所持金{_globalParameter.Money}円");
    }
    public override void PutLuggage(Collision collision)
    {
        //Empty
    }

    private void Start()
    {
        _store = FindAnyObjectByType<Store>();
        _globalParameter = FindAnyObjectByType<GlobalParameters>();
    }

}
