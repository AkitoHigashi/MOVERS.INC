using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Data/ItemData")]
public class ItemDataList : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        [SerializeField] GameObject _item;
        [SerializeField, Tooltip("�w���R�X�g")] int _purchaseCost;
        [SerializeField] ItemType _type;

        public enum ItemType
        {
            [InspectorName("�����؂�")] Endless,
            [InspectorName("���Օi")] Consume
        }
    }
}
