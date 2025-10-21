namespace ProcessInterface
{
    /// <summary>攻撃を食らうものが継承するインターフェース</summary>
    public interface IHittable
    {
        /// <summary>ダメージを受ける関数</summary>
        /// <param name="power">ダメージ</param>
        public void TakeDamage(float power);
    }

    /// <summary>攻撃するものが継承するインターフェース</summary>
    public interface IAttackable
    {
        /// <summary>攻撃力を受け取るプロパティ</summary>
        public float Power {  get; }

        /// <summary>攻撃する関数</summary>
        /// <returns>ダメージの総量</returns>
        public float Attack();
    }
}
