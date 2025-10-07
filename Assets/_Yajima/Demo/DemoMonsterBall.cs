using UnityEngine;

/// <summary>
/// �����X�^�[�{�[���i���j
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DemoMonsterBall : ItemBase
{
    Rigidbody _rb;

    private void Start()
    {
        SetUp();
    }

    protected override void SetUp()
    {
        base.SetUp();
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    public override void ItemActivate()
    {
        _rb.isKinematic = false;
        _rb.AddForce(GameObject.Find("DemoPlayer").transform.forward, ForceMode.Impulse);
        Debug.Log("�{�[���𓊂���");
    }
}
