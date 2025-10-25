using UnityEngine;

public class PlayerItemUse : MonoBehaviour
{
    private LuggageData _luggageData;
    public void ItemUse()
    {
        if (_luggageData.TryGetComponent<ItemBase>(out var itemEffect))
        {
            _luggageData.LuggageRb.isKinematic = false;
            _luggageData.LuggageGameObject.transform.SetParent(null);
            _luggageData.LuggageScript = null;
            itemEffect.ItemActivate();
            Debug.Log("ItemUse");
        }
    }
}
