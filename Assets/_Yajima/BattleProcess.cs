using ProcessInterface;

/// <summary>戦闘過程を司るクラス</summary>
public static class BattleProcess
{
    /// <summary>
    /// ダメージ計算を行うクラス
    /// </summary>
    /// <param name="attack">攻撃側</param>
    /// <param name="hit">ダメージを受ける側</param>
    public static void DamageCalcurate(IAttackable attack,IHittable hit)
    {
        hit.TakeDamage(attack.Attack());
    }
}
