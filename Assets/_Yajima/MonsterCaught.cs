using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterCaught : MonoBehaviour
{
    [SerializeField] int _durabilty = 10;
    GameObject _monster;
    ItemData _data;

    /// <summary>
    /// 捕まえた情報を保持する関数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="data"></param>
    public void MonsterCatch(GameObject obj, ItemData data)
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
            _monster.SetActive(true);
            _monster.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
