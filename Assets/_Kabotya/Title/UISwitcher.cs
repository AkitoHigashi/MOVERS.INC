using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _firstObject;
    [SerializeField] private GameObject _secondObject;

    public void NextUI()
    {
        // _firstObjectがアクティブかどうかで判断
        if (_firstObject.activeSelf)
        {
            _firstObject.SetActive(false);
            _secondObject.SetActive(true);
        }
        else
        {
            _firstObject.SetActive(true);
            _secondObject.SetActive(false);
        }
    }
}
