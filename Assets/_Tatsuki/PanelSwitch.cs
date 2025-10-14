using UnityEngine;

public class PanelSwitch : MonoBehaviour
{

    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject OutGameUI;


    public void SetPanelButton()
    {
       InGameUI.SetActive(false);
       OutGameUI.SetActive(true);

    }
}
