using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterCaught : MonoBehaviour
{
    [SerializeField, Tooltip("耐久力")] int _durabilty = 10;
    MonsterBase _monster;
    //なんで用意したかわからないけど多分使うんだと思う
    ItemData _data;

    /// <summary>
    /// 捕まえた情報を保持する関数
    /// </summary>
    /// <param name="obj">捕まえたモンスターの情報</param>
    /// <param name="data"></param>
    public void MonsterCatch(MonsterBase obj, ItemData data)
    {
        _data = data;
        _monster = obj;
    }

    /// <summary>
    /// ダメージを受ける関数
    /// </summary>
    public void TakeDamage()
    {
        _durabilty--;
        if (_durabilty <= 0)
        {
            _monster.gameObject.SetActive(true);
            _monster.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
