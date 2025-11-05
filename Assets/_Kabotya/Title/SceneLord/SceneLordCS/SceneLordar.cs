using UnityEngine;

public class SceneLordar : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    public void OnPlayButtonClick()
    {
        SceneLordManager.Instance.Scnenlode(nextSceneName);
    }
}
