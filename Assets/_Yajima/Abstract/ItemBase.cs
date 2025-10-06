using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    /// <summary>
    /// �����ݒ���s���֐��ŁA�K��base���Ăяo������
    /// </summary>
    protected virtual void SetUp()
    {
        if (tag != "Item")
        {
            tag = "Item";
        }
    }

    /// <summary>
    /// �A�C�e���̌��ʂ𔭓�����֐�
    /// </summary>
    public abstract void ItemActivate();
}
