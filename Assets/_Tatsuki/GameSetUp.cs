using UnityEngine;

/// <summary>
/// CollectionArea と LuggageManager のイベントを紐づける初期設定用クラス。
/// OnEnable 時にイベント登録、OnDisable 時に解除を行う。
/// </summary>
public class GameSetUp : MonoBehaviour
{
    [SerializeField] private CollectionArea _collectionArea;
    [SerializeField] private LuggageManager  _luggageManager;

    private void OnEnable()
    {
        // 荷物がエリアに入った/出た時のイベント登録
        _collectionArea.OnEnter += _luggageManager.RegisterItem;
        _collectionArea.OnExit += _luggageManager.UnregisterItem;
    }

    private void OnDisable()
    {
        // イベントの解除
        _collectionArea.OnEnter -= _luggageManager.RegisterItem;
        _collectionArea.OnExit -= _luggageManager.UnregisterItem;
    }
}
