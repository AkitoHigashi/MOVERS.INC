using UnityEngine;

public class TrapDemolished : MonoBehaviour
{
    [SerializeField] LuggageTrap _luggageTrap;
    [SerializeField, Tooltip("ツールキットとの距離に対する有効範囲")] float _effectiveRange = 1;

    private void Start()
    {
        if (tag != "TrapBase")
        {
            tag = "TrapBase";
        }
    }

    /// <summary>
    /// 解体されたとき呼び出す関数
    /// </summary>
    public void Demolished(Transform pos)
    {
        if (Vector3.Distance(pos.position, transform.position) <= _effectiveRange)
        {
            //Luggageタグが付いたトラップを生成
            var go = Instantiate(_luggageTrap);
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            //このトラップを消去
            gameObject.SetActive(false);
        }
    }
}
