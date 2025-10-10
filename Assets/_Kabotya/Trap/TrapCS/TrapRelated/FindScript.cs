using UnityEngine;

public class FindScript : MonoBehaviour
{
    /// <summary>
    /// 指定したコンポーネントを、オブジェクトの親 → 子の順で探して返す。
    /// </summary>
    public static T FindInParentOrChildren<T>(GameObject obj) where T : Component
    {
        //親を探す
        T component = obj.GetComponentInParent<T>();
        if (component == null)
        // 親に無ければ子を探す
        component = obj.GetComponentInChildren<T>();
        return component;
    }
}
