using UnityEngine;

/// <summary>
/// モンスターボール（仮）
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DemoMonsterBall : ItemBase
{
    Rigidbody _rb;

    protected override void Init()
    {
        base.Init();
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    public override void ItemActivate()
    {
        _rb.isKinematic = false;
        _rb.AddForce(GameObject.Find("DemoPlayer").transform.forward, ForceMode.Impulse);
        Debug.Log("ボールを投げた");
    }
}
