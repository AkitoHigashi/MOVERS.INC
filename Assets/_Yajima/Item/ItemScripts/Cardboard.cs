using UnityEngine;
using static LuggageGenerator;

public class Cardboard : ItemBase
{
    [SerializeField] CardboardInstance _cardboardInstance;
    public override void ItemActivate(LuggageData luggage)
    {
        luggage.LuggageRb.isKinematic = false;
        luggage.LuggageScript = null;
        luggage.LuggageGameObject = null;
        Instantiate(_cardboardInstance, _cameraTrans.position + Vector3.forward, Quaternion.identity);
        _playerCarry.CarryingBoolFalse();
        //StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
