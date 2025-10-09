using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtonEvent : MonoBehaviour
{
    [SerializeField] private InputField _inputDayCount;
    [SerializeField] private InputField _inputRank;
    [SerializeField] private InputField _inputMoney;
    
    private GlobalParameters _gp;

    private void Start()
    {
        _gp = GlobalParameters.Instance;
    }

    public void OnGotoLobbyClick()
    {
        _gp.Init(
            int.Parse(_inputDayCount.text),
            int.Parse(_inputMoney.text),
            int.Parse(_inputRank.text));
        SceneManager.LoadScene("_Chou/Test_Lobby");
    }
}