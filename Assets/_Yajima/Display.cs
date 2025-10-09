using UnityEngine;

/// <summary>
/// ストアに並べるオブジェクトにアタッチするスクリプト
/// </summary>
public class Display : MonoBehaviour
{
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

    private void Start()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }
    }
}
