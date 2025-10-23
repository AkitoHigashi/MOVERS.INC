using UnityEngine;

public class PanelSwitch : MonoBehaviour
{

    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _outGameUI;


    public void SetPanelButton()
    {
       _inGameUI.SetActive(false);
       _outGameUI.SetActive(true);

    }
}
