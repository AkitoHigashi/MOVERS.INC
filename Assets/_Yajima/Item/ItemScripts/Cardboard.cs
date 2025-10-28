using UnityEngine;

public class Cardboard : ItemBase
{
    [SerializeField] CardboardInstance _cardboardInstance;
    public override void ItemActivate(LuggageData luggage)
    {
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
