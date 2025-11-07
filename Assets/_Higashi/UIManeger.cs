using UnityEngine;

public class UIManeger : MonoBehaviour
{
    [SerializeField] private GameObject _commonUI;
    [SerializeField] private GameObject _lobbyUI;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _resultUI;

    public void ShowLobbyUI()
    {
        _commonUI.SetActive(true);
        _lobbyUI.SetActive(true);
        _inGameUI.SetActive(false);
        _resultUI.SetActive(false);
    }
    public void ShowInGameUI()
    {
        _commonUI.SetActive(true);
        _lobbyUI.SetActive(false);
        _inGameUI.SetActive(true);
        _resultUI.SetActive(false);
    }
    public void ShowResultUI()
    {
        _commonUI.SetActive(false);
        _lobbyUI.SetActive(false);
        _inGameUI.SetActive(false);
        _resultUI.SetActive(true);
    }
}
