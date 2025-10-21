using UnityEngine;

public class MonsterSphere : ItemBase
{
    [SerializeField] float _throwPower = 10;
    [SerializeField] GameObject _caughtSphere;
    GameObject _monster;

    /// <summary>
    /// モンスターを捕獲する関数
    /// </summary>
    /// <param name="monster">捕獲するモンスター</param>
    void GetMonster(GameObject monster)
    {
        //モンスターを捕まえた瞬間の処理
        _monster = monster;
        _monster.SetActive(false);
        gameObject.SetActive(false);
        //捕まえた直後の処理
        Instantiate(_caughtSphere, transform.position, Quaternion.identity);
        _caughtSphere.GetComponent<MonsterCaught>().MonsterCatch(monster, _itemdata);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_monster)
        {
            if (collision.gameObject.tag == "Monster")
            {
                var enemy = collision.gameObject.GetComponent<EnemyBase>();
                if (enemy.EnemyData.CanGet(enemy.EnemyHP))
                {
                    //捕まえられるHPなら
                    GetMonster(enemy.gameObject);
                }
            }
        }
    }

    public override void ItemActivate()
    {
        _rb.isKinematic = false;
        transform.SetParent(null);
        //カメラの中心から前方に飛ばす
        transform.position = Camera.main.transform.position;
        _rb.AddForce(Camera.main.transform.forward * _throwPower, ForceMode.Impulse);
        //アイテムを使った情報を保管庫に知らせる
        StorageData.ItemUse(_itemdata);
    }
}
