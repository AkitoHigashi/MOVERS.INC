using UnityEngine;

public class InActiveButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void OnInActiveButtonPressed()
    {
        targetObject.SetActive(false);
    }
}
