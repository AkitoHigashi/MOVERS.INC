using UnityEngine;

public class SweepInstance : ItemBase
{
    [SerializeField] GameObject _item;
    [SerializeField] Animator _anim;
    [SerializeField] string _animName;

    protected override void Init()
    {
        base.Init();
        //_rb.isKinematic = true;
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _anim.Play(_animName);
    }

    public void SEPlay()
    {
        SEManager.SEPlay("Attack");
    }

    public void ActivateEnd()
    {
        _item.SetActive(true);
        transform.SetParent(_item.transform);
        gameObject.SetActive(false);
    }

    public override void ItemActivate(LuggageData luggage)
    {
        //Empty
    }
}
