using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// スライム特有の動きを制御するクラス
/// </summary>
public class Slime : EnemyBase
{
    private void Awake()
    {
        base.BaseAwake();
    }
    private void Update()
    {
        base.BaseUpdate();

        SetAnimation();
    }
    private void OnEnable()
    {
        base.BaseOnEnable();
    }
    private void OnDisable()
    {
        base.BaseOnDisable();
    }
    private void SetAnimation()
    {
        _animator.SetFloat("WalkSpeed", _navMeshAgent.speed);
    }
}
