using UnityEngine;

public class Cardboard : ItemBase
{
    [SerializeField] CardboardInstance _cardboardInstance;
    public override void ItemActivate()
    {
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
