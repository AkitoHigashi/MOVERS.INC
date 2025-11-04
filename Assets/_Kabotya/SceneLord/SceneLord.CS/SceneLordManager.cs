using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLordManager : MonoBehaviour
{
    public static SceneLordManager Instance;
    private FadeoutTrigger _fadeoutTrigger;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _fadeoutTrigger = GetComponent<FadeoutTrigger>();
    }
    public void Scnenlode(string name)
    {
        _fadeoutTrigger.OnFadeButtonPressed();
        SceneManager.LoadScene(name);
    }
}
