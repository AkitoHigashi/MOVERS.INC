using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLordManager : MonoBehaviour
{
    public static SceneLordManager Instance;
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

    public void Scnenlode(string sceneName) 

    {
        SceneManager.LoadScene(sceneName);
    }
}
