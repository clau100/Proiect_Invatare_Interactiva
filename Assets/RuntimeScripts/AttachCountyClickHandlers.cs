using UnityEditor;
using UnityEngine;

public class AttachCountyClickHandlers : MonoBehaviour
{
    [MenuItem("Tools/Attach Click Handlers to Counties")]
    static void AttachClickHandlers()
    {
        var allSpriteRenderers = Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);

        foreach (var renderer in allSpriteRenderers)
        {
            GameObject countyObject = renderer.gameObject;

            // Add Collider2D if not already
            if (countyObject.GetComponent<Collider2D>() == null)
            {
                countyObject.AddComponent<PolygonCollider2D>();
            }

            // Add Click Handler if not already
            if (countyObject.GetComponent<CountyClickHandler>() == null)
            {
                countyObject.AddComponent<CountyClickHandler>();
                Debug.Log($"✅ Click handler added to: {countyObject.name}");
            }

            if (countyObject.GetComponent<CountyClickHandler>().outlineMaterial == null)
            {
                countyObject.GetComponent<CountyClickHandler>().outlineMaterial = Resources.Load<Material>("Materials/OutlineMaterial");
            }
        }

        Debug.Log("🎉 All counties now have click handlers!");
    }
}
