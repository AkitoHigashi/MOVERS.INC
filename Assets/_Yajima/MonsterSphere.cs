using UnityEngine;

public class MonsterSphere : ItemBase
{
    [SerializeField, Tooltip("投げる時に前方向にかかる力")] float _throwPowerForward = 10;
    [SerializeField, Tooltip("投げる時に上方向にかかる力")] float _throwPowerUp = 3;
    [SerializeField, Tooltip("捕まえた時に出すオブジェクト")] MonsterCaught _caughtSphere;
    MonsterBase _monster;

    /// <summary>
    /// モンスターを捕獲する関数
    /// </summary>
    /// <param name="monster">捕獲するモンスター</param>
    void GetMonster(MonsterBase monster)
    {
        //モンスターを捕まえた瞬間の処理
        _monster = monster;
        _monster.gameObject.SetActive(false);
        gameObject.SetActive(false);
        //捕まえた直後の処理
        Instantiate(_caughtSphere, transform.position, Quaternion.identity);
        _caughtSphere.MonsterCatch(monster, _itemdata);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_monster)
        {
            if (collision.gameObject.tag == "Monster")
            {
                var enemy = collision.gameObject.GetComponent<MonsterBase>();
                if (enemy.EnemyData.CanGet(enemy.MonsterHP))
                {
                    //捕まえられるHPなら
                    GetMonster(enemy);
                }
            }
        }
    }

    [ContextMenu("ItemActivate")]
    public override void ItemActivate()
    {
        _rb.isKinematic = false;
        transform.SetParent(null);
        //カメラの中心から前方に飛ばす
        _rb.linearVelocity = Vector3.zero;
        transform.position = Camera.main.transform.position;
        _rb.AddForce(Camera.main.transform.forward * _throwPowerForward + Camera.main.transform.up * _throwPowerUp, ForceMode.Impulse);
        //アイテムを使った情報を保管庫に知らせる
        StorageData.ItemUse(_itemdata);
    }
}
