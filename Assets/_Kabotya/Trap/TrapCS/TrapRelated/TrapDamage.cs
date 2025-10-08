using UnityEngine;

public class TrapDamage: MonoBehaviour
{
    [SerializeField] private int _trapDamage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{_trapDamage++}");
        //ここに条件とダメージ計算をするメソッドを書く
    }
}
