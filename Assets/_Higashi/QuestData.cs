using UnityEngine;
/// <summary>
/// クエストのUIに渡す。依頼詳細
/// </summary>
[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public Sprite QuestImage;//迷宮のイメージ画像
    public string QuestName;//クエスト（迷宮）の名前
    [TextArea]
    public string QuestDescription;//クエストの依頼文
    public int RequiredDeliveryCount;//荷物の搬入指定数
    public int Reward;//推定報酬
    public string QuestSceneName;//出撃するシーン　シーン遷移用
    [Header("トラップの情報")]
    public QuestTrapData[] Traps;
}
