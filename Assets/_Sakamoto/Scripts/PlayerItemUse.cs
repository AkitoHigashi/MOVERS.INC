using UnityEngine;

public class PlayerItemUse : MonoBehaviour
{
    private LuggageData _luggageData;

    private void Start()
    {
        _luggageData = GetComponent<LuggageData>();
    }
    public void ItemUse()
    {
        if (_luggageData.LuggageGameObject.TryGetComponent<ItemBase>(out var itemEffect))
        {
            //_luggageData.LuggageRb.isKinematic = false;
            //_luggageData.LuggageScript = null;
            itemEffect.ItemActivate(_luggageData);
            Debug.Log("ItemUse");
        }
    }
}
