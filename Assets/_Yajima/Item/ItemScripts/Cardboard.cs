using UnityEngine;

public class Cardboard : ItemBase
{
    [SerializeField] CardboardInstance _cardboardInstance;
    public override void ItemActivate(LuggageData luggage)
    {
        luggage.LuggageRb.isKinematic = false;
        luggage.LuggageScript = null;
        Instantiate(_cardboardInstance, _cameraTrans.position + Vector3.forward, Quaternion.identity);
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
