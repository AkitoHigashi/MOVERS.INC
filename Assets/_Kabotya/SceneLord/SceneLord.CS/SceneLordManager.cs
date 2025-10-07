using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLordManager : MonoBehaviour
{
    public static SceneLordManager Instance;
    public void Scnenlode(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
