using UnityEngine;

public class TrapDamage: MonoBehaviour
{
    [SerializeField] private int _trapDamage = 1;

    private void OnCollisionEnter(Collision collision) 
    {
        Debug.Log($"うんこが{_trapDamage}個");
        //ここに条件とダメージ計算をするメソッドを書く
    }
}
