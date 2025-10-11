using System;
using UnityEngine;
/// <summary>
///　クエストのUIにそのトラップが存在するのかを表示する為に作られたトラップのデータ
/// </summary>
[Serializable]
public class QuestTrapData
{
    [Header("トラップ情報")]
    public Sprite TrapIcon;
    [Header("クエストに設置されているのか")]
    public bool IsPlacedInQuset;
    //public string TrapName; 今使わないかも
    //public GameObject TrapPrefab; いま使わないかも
}
