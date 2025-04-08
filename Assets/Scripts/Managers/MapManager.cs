using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // So it persists between scenes
        }
    }

    public void LoadSection(string sectionName)
    {
        Debug.Log("Loading section: " + sectionName);
        SceneManager.LoadScene(sectionName);
    }
}
