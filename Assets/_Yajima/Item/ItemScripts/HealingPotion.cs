using UnityEngine;

public class HealingPotion : ItemBase
{
    [SerializeField] float _heal;
    public float Heal => _heal;
    PotionGarbageCollecter _pgc;
    protected override void Init()
    {
        base.Init();
        _pgc = FindFirstObjectByType<PotionGarbageCollecter>();
    }

    [ContextMenu("a")]
    public override void ItemActivate()
    {
        _pgc.HealingPotion(this);
        StorageData.ItemUse(ItemData);
        Destroy(gameObject);
    }
}
