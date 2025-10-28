using UnityEngine;

public class Cardboard : ItemBase
{
    [SerializeField] CardboardInstance _cardboardInstance;
    [SerializeField] Vector3 _pos;
    public override void ItemActivate(LuggageData luggage)
    {
        Instantiate(_cardboardInstance, _pos, Quaternion.identity);
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
