using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CountyClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    public Material outlineMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.sharedMaterial;
    }

    private void OnMouseEnter()
    {
        if (outlineMaterial != null)
            spriteRenderer.material = outlineMaterial;
    }

    private void OnMouseExit()
    {
        spriteRenderer.material = originalMaterial;
    }

    private void OnMouseDown()
    {
        string sceneName = gameObject.name;
        sceneName += "Sprite";
        Debug.Log($"➡️ Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
