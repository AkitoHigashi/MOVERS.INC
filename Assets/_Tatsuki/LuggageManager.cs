using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 現在コレクションエリア内に存在する荷物を管理するクラス。
/// 登録・解除・リスト取得を行う。
/// </summary>
public class LuggageManager : MonoBehaviour
{
    private List<GameObject> _itemArea = new List<GameObject>();

    // 荷物をエリア内リストに登録
    public void RegisterItem(GameObject item)
    {
        _itemArea.Add(item);
    }

    // 荷物をエリア内リストから削除
    public void UnregisterItem(GameObject item)
    {
        _itemArea.Remove(item);
    }

    // 現在のエリア内の荷物一覧を取得
    public List<GameObject> GetItemInArea() => new List<GameObject>(_itemArea);
}
