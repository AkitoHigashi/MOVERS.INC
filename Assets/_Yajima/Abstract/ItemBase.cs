using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    /// <summary>
    /// 初期設定を行う関数で、必ずbaseも呼び出すこと
    /// </summary>
    protected virtual void SetUp()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }
    }

    /// <summary>
    /// アイテムの効果を発動する関数
    /// </summary>
    public abstract void ItemActivate();
}
