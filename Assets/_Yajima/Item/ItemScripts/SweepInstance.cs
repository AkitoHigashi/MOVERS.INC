using UnityEngine;

public class SweepInstance : ItemBase
{
    [SerializeField] float _power;
    [SerializeField] GameObject _item;
    [SerializeField] Animator _anim;
    [SerializeField] string _animName;
    public float Power => _power;

    protected override void Init()
    {
        base.Init();
        _rb.isKinematic = true;
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _anim.Play(_animName);
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
