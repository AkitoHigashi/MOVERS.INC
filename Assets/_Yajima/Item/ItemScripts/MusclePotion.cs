using UnityEngine;

public class MusclePotion : ItemBase
{
    [SerializeField, Tooltip("秒")] float _effectiveTime = 5;
    public float EffectiveTime => _effectiveTime;
    PotionGarbageCollecter _pgc;

    protected override void Init()
    {
        base.Init();
        _pgc = FindFirstObjectByType<PotionGarbageCollecter>();
    }

    [ContextMenu("a")]
    public override void ItemActivate(LuggageData luggage)
    {
        luggage.LuggageRb.isKinematic = false;
        luggage.LuggageScript = null;
        _pgc.MusclePotion(this);
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
