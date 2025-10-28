using UnityEngine;

public class SweepInstance : ItemBase
{
    [SerializeField] float _power;
    [SerializeField] GameObject _item;
    public float Power => _power;

    protected override void Init()
    {
        base.Init();
        //transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity;
        _rb.isKinematic = true;
    }

    public void ActivateEnd()
    {
        _item.SetActive(true);
        transform.SetParent(_item.transform);
        gameObject.SetActive(false);
    }

    public override void ItemActivate()
    {
        //Empty
    }
}
