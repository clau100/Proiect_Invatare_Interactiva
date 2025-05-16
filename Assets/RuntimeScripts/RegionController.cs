using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class RegionClickHandler : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Configurable scene name to load
    public Material outlineMaterial;
    private SpriteRenderer[] countyRenderers;
    private Dictionary<string, Material> countyMaterials = new Dictionary<string, Material>();

    // Reusing your existing region color data
    static readonly Dictionary<string, Color> regionData = new Dictionary<string, Color>
{
        // Crisana - Royal Blue
        { "Arad",               new Color(65.0f / 255.0f, 105.0f / 255.0f, 225.0f / 255.0f, 1.0f)},
        { "Bihor",              new Color(65.0f / 255.0f, 105.0f / 255.0f, 225.0f / 255.0f, 1.0f)},
    
        // Dobrogea - Turquoise
        { "Constanta",          new Color(64.0f / 255.0f, 224.0f / 255.0f, 208.0f / 255.0f, 1.0f)},
        { "Tulcea",             new Color(64.0f / 255.0f, 224.0f / 255.0f, 208.0f / 255.0f, 1.0f)},
    
        // Banat - Tomato Red
        { "Timis",              new Color(255.0f / 255.0f, 99.0f / 255.0f, 71.0f / 255.0f, 1.0f)},
        { "Caras-Severin",      new Color(255.0f / 255.0f, 99.0f / 255.0f, 71.0f / 255.0f, 1.0f)},
    
        // Bucovina - Lime Green
        { "Botosani",           new Color(50.0f / 255.0f, 205.0f / 255.0f, 50.0f / 255.0f, 1.0f)},
        { "Suceava",            new Color(50.0f / 255.0f, 205.0f / 255.0f, 50.0f / 255.0f, 1.0f)},
    
        // Moldova - Deep Orange
        { "Iasi",               new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
        { "Vaslui",             new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
        { "Galati",             new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
        { "Vrancea",            new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
        { "Neamt",              new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
        { "Bacau",              new Color(255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 1.0f)},
    
        // Maramures - Indigo
        { "Satu Mare",          new Color(75.0f / 255.0f, 0.0f / 255.0f, 130.0f / 255.0f, 1.0f)},
        { "Maramures",          new Color(75.0f / 255.0f, 0.0f / 255.0f, 130.0f / 255.0f, 1.0f)},
    
        // Oltenia - Crimson
        { "Mehedinti",          new Color(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Dolj",               new Color(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Olt",                new Color(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Gorj",               new Color(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Vâlcea",             new Color(220.0f / 255.0f, 20.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
    
        // Muntenia - Orchid Purple
        { "Calarasi",           new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Teleorman",          new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Giurgiu",            new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Bucharest",          new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Ialomita",           new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Braila",             new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Ilfov",              new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Arges",              new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Prahova",            new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Buzau",              new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Dâmbovita",          new Color(186.0f / 255.0f, 85.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
    
        // Transilvania - Forest Green
        { "Cluj",               new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Bistrita-Nasaud",    new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Salaj",              new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Hunedoara",          new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Covasna",            new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Brasov",             new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Sibiu",              new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Mures",              new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Harghita",           new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
        { "Alba",               new Color(34.0f / 255.0f, 139.0f / 255.0f, 34.0f / 255.0f, 1.0f)},
    };

    private void Awake()
    {
        // If sceneToLoad is not set, default to the region's name
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            sceneToLoad = gameObject.name;
        }

        // Get all child SpriteRenderers (counties)
        countyRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Initialize materials for each county
        foreach (SpriteRenderer renderer in countyRenderers)
        {
            // Create a new material instance for each county
            Material countyMaterial = new Material(outlineMaterial);

            // Set initial unfocused color (using the county name to look up in regionData)
            if (regionData.TryGetValue(renderer.gameObject.name, out Color countyColor))
            {
                Color unfocusedColor = countyColor;
                unfocusedColor.a = 0.3f;
                countyMaterial.SetColor("_OutlineColor", unfocusedColor);
            }

            // Apply the material to the county and store it for later use
            renderer.material = countyMaterial;
            countyMaterials[renderer.gameObject.name] = countyMaterial;
        }
    }

    private void OnMouseEnter()
    {
        // Highlight all counties in the region
        foreach (SpriteRenderer renderer in countyRenderers)
        {
            if (regionData.TryGetValue(renderer.gameObject.name, out Color countyColor))
            {
                renderer.material.SetColor("_OutlineColor", countyColor);
            }
        }

        // Update UI text with region name
        TMP_Text countryUIText = GameObject.Find("CountryUI").GetComponent<TMP_Text>();
        countryUIText.text = gameObject.name;
    }

    private void OnMouseExit()
    {
        // Return all counties to unfocused state
        foreach (SpriteRenderer renderer in countyRenderers)
        {
            if (regionData.TryGetValue(renderer.gameObject.name, out Color countyColor))
            {
                Color unfocusedColor = countyColor;
                unfocusedColor.a = 0.3f;
                renderer.material.SetColor("_OutlineColor", unfocusedColor);
            }
        }

        // Clear UI text
        TMP_Text countryUIText = GameObject.Find("CountryUI").GetComponent<TMP_Text>();
        countryUIText.text = "";
    }

    private void OnMouseDown()
    {
        // Load the configured scene
        Debug.Log($"➡️ Loading scene: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }
}