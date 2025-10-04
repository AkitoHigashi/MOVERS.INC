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
    /// �A�C�e�����g�p����֐�
    /// </summary>
    public abstract void UseItem();
}
