using UnityEngine;

/// <summary>
/// ゴーレム特有の動きを制御するクラス
/// </summary>
public class Golem : EnemyBase
{
    protected override void ProccesToPlayer(Collider collider, float distance)
    {
        _currentDestination = collider.transform.position;
        if (distance < _stopDistance)
        {
            switch (_currentEnemyState)
            {
                case EnemyState.Hostile:
                    Attack(collider);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// プレイヤーに攻撃
    /// </summary>
    /// <param name="player"></param>
    private void Attack(Collider player)
    {
        //アニメーションとか攻撃を走らせる
        Debug.Log($"{this.name}の攻撃");
    }
}
