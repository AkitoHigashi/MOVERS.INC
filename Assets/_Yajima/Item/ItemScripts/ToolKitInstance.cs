using UnityEngine;

public class ToolKitInstance : ItemBase
{
    [SerializeField] ToolKit _tool;
    [SerializeField] Animator _anim;
    [SerializeField] string _animName;

    public override void ItemActivate()
    {
        //Empty
    }

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

    public void EffectEnd()
    {
        _tool.gameObject.SetActive(true);
        transform.SetParent(_tool.transform);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrapBase")
        {
            //トラップ解体
            other.gameObject.GetComponent<TrapBase>().Demolished();
        }
    }
}
