using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLordManager : MonoBehaviour
{
    void Scnenlode(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
