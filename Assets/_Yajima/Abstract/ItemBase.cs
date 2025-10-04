using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected virtual void SetUp()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    public abstract void UseItem();
}
