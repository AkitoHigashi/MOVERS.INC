using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public float TrapDamage => _trapDamage;
    [SerializeField] private float _trapDamage = 0;
    [SerializeField] LuggageTrap _luggageTrap;

    /// <summary>
    /// 解体されたとき呼び出す関数
    /// </summary>
    public void Demolished()
    {
        //Luggageタグが付いたトラップを生成
        var go = Instantiate(_luggageTrap);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        //このトラップを消去
        gameObject.SetActive(false);
    }
}
