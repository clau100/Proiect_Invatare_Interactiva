using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CountyClickHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sceneName = gameObject.name;
        sceneName += "Sprite";
        Debug.Log($"➡️ Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
