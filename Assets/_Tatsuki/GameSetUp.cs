using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    [SerializeField] private CollectionArea CollectionArea;
    [SerializeField] private TatukiItemManager TatukiItemManager;

    private void OnEnable()
    {
        CollectionArea.OnEnter += TatukiItemManager.RegisterItem;   
        CollectionArea.OnExit += TatukiItemManager.UnregisterItem;

    }
    private void OnDisable()
    {
        CollectionArea.OnEnter -= TatukiItemManager.RegisterItem;
        CollectionArea.OnExit -= TatukiItemManager.UnregisterItem;
    }
}
