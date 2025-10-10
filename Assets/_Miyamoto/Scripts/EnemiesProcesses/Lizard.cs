using UnityEngine;

/// <summary>
/// リザード特有の動きを制御するクラス
/// </summary>
public class Lizard : EnemyBase
{
    protected override void ProccesToLuggage(Collider collider, float distance)
    {
        _currentDestination = collider.transform.position;
        if (distance < _stopDistance)
        {
            switch (_currentEnemyState)
            {
                case EnemyState.Neutral:
                    CarryBaggage(collider);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 荷物を拾って運ぶ処理
    /// </summary>
    /// <param name="baggage"></param>
    private void CarryBaggage(Collider baggage)
    {
        //今後追加予定
        Debug.Log("荷物を運ぶ");
    }
}