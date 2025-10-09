using UnityEngine;

public class MonsterSphere : ItemBase
{
    GameObject _monster;

    /// <summary>
    /// モンスターを捕獲する関数
    /// </summary>
    /// <param name="monster">捕獲するモンスター</param>
    void GetMonster(GameObject monster)
    {
        _monster = monster;
        _monster.SetActive(false);
    }

    /// <summary>
    /// モンスターを解放する関数
    /// </summary>
    void ReleaseMonster()
    {
        _monster.SetActive(true);
        _monster = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            var enemy = collision.gameObject.GetComponent<EnemyBase>();
            //if(enemy.EnemyData.CanGet(enemy.)){
            //  GetMonster(collision.gameObject);
            //}
        }
    }

    public override void ItemActivate()
    {
        
    }
}
