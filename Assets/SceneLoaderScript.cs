using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public string sceneToLoad = "";

    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
