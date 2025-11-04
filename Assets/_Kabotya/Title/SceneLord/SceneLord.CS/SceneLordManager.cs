using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLordManager : MonoBehaviour
{
    void Awake()
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
    }

    public static SceneLordManager Instance;
    public void Scnenlode(string sceneName) 

    {
        SceneManager.LoadScene(sceneName);
    }
}
