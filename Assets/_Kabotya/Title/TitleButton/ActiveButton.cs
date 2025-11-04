using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    [SerializeField]GameObject targetObject;

    public void OnActiveButtonPressed()
    {
        targetObject.SetActive(true);
    }
}
