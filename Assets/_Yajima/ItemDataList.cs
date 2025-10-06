using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Data/ItemData")]
public class ItemDataList : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        [SerializeField] GameObject _item;
        [SerializeField, Tooltip("購入コスト")] int _purchaseCost;
        [SerializeField] ItemType _type;

        public enum ItemType
        {
            [InspectorName("買い切り")] Endless,
            [InspectorName("消耗品")] Consume
        }
    }
}
