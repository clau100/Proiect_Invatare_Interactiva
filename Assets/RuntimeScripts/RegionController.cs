using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class RegionClickHandler : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string sceneToLoad = "";

    [Header("Materials & Colors")]
    [SerializeField] private Material outlineMaterial;
    static readonly Dictionary<string, Color> regionData = new Dictionary<string, Color>
    {
        // Crisana – Royal Blue
        { "Arad",              new Color(65f/255f, 105f/255f, 225f/255f, 1f) },
        { "Bihor",             new Color(65f/255f, 105f/255f, 225f/255f, 1f) },

        // Dobrogea – Turquoise
        { "Constanta",         new Color(64f/255f, 224f/255f, 208f/255f, 1f) },
        { "Tulcea",            new Color(64f/255f, 224f/255f, 208f/255f, 1f) },

        // Banat – Tomato Red
        { "Timis",             new Color(255f/255f,  99f/255f,  71f/255f, 1f) },
        { "Caras-Severin",     new Color(255f/255f,  99f/255f,  71f/255f, 1f) },

        // Bucovina – Lime Green
        { "Botosani",          new Color(50f/255f, 205f/255f,  50f/255f, 1f) },
        { "Suceava",           new Color(50f/255f, 205f/255f,  50f/255f, 1f) },

        // Moldova – Deep Orange
        { "Iasi",              new Color(255f/255f, 140f/255f,   0f/255f, 1f) },
        { "Vaslui",            new Color(255f/255f, 140f/255f,   0f/255f, 1f) },
        { "Galati",            new Color(255f/255f, 140f/255f,   0f/255f, 1f) },
        { "Vrancea",           new Color(255f/255f, 140f/255f,   0f/255f, 1f) },
        { "Neamt",             new Color(255f/255f, 140f/255f,   0f/255f, 1f) },
        { "Bacau",             new Color(255f/255f, 140f/255f,   0f/255f, 1f) },

        // Maramures – Indigo
        { "Satu Mare",         new Color(75f/255f,   0f/255f, 130f/255f, 1f) },
        { "Maramures",         new Color(75f/255f,   0f/255f, 130f/255f, 1f) },

        // Oltenia – Crimson
        { "Mehedinti",         new Color(220f/255f,  20f/255f,  60f/255f, 1f) },
        { "Dolj",              new Color(220f/255f,  20f/255f,  60f/255f, 1f) },
        { "Olt",               new Color(220f/255f,  20f/255f,  60f/255f, 1f) },
        { "Gorj",              new Color(220f/255f,  20f/255f,  60f/255f, 1f) },
        { "Vâlcea",            new Color(220f/255f,  20f/255f,  60f/255f, 1f) },

        // Muntenia – Orchid Purple
        { "Calarasi",          new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Teleorman",         new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Giurgiu",           new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Bucharest",         new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Ialomita",          new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Braila",            new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Ilfov",             new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Arges",             new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Prahova",           new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Buzau",             new Color(186f/255f,  85f/255f, 211f/255f, 1f) },
        { "Dâmbovita",         new Color(186f/255f,  85f/255f, 211f/255f, 1f) },

        // Transilvania – Forest Green
        { "Cluj",              new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Bistrita-Nasaud",   new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Salaj",             new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Hunedoara",         new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Covasna",           new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Brasov",            new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Sibiu",             new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Mures",             new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Harghita",          new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
        { "Alba",              new Color(34f/255f, 139f/255f,  34f/255f, 1f) },
    };

    [Header("Unselected State")]
    [Range(0f, 1f)][SerializeField] private float unselectedFillAlpha = 0.2f;
    [Range(0f, 1f)][SerializeField] private float unselectedOutlineAlpha = 0.5f;
    [SerializeField] private float unselectedOutlineWidth = 1f;
    [SerializeField] private Vector3 unselectedScale = Vector3.one;

    [Header("Selected State")]
    [Range(0f, 1f)][SerializeField] private float selectedFillAlpha = 1f;
    [Range(0f, 1f)][SerializeField] private float selectedOutlineAlpha = 1f;
    [SerializeField] private float selectedOutlineWidth = 5f;
    [SerializeField] private Vector3 selectedScale = new Vector3(1.05f, 1.05f, 1f);

    private SpriteRenderer[] countyRenderers;
    private static readonly List<RegionClickHandler> allRegions = new List<RegionClickHandler>();
    private static RegionClickHandler currentlySelected;

    private void Awake()
    {
        // register this instance
        allRegions.Add(this);

        // default scene if none set
        if (string.IsNullOrEmpty(sceneToLoad))
            sceneToLoad = gameObject.name;

        // cache renderers and apply unselected look
        countyRenderers = GetComponentsInChildren<SpriteRenderer>();
        ApplyState(unselectedFillAlpha, unselectedOutlineAlpha, unselectedOutlineWidth);
        transform.localScale = unselectedScale;
    }

    private void OnDestroy()
    {
        allRegions.Remove(this);
    }

    private void OnMouseEnter()
    {
        var res = GameManager.Instance.completedPuzzles;
        foreach(var a in res)
        {
            Debug.Log(a);
        }

        // hover highlight only if not selected
        if (currentlySelected != this)
            ApplyState(selectedFillAlpha, selectedOutlineAlpha * 0.7f, unselectedOutlineWidth);

        var ui = GameObject.Find("CountryUI")?.GetComponent<TMP_Text>();
        if (ui != null) ui.text = gameObject.name;
    }

    private void OnMouseExit()
    {
        if (currentlySelected != this)
            ApplyState(unselectedFillAlpha, unselectedOutlineAlpha, unselectedOutlineWidth);

        var ui = GameObject.Find("CountryUI")?.GetComponent<TMP_Text>();
        if (ui != null) ui.text = "";
    }

    private void OnMouseDown()
    {
        // reset all to unselected
        foreach (var region in allRegions)
        {
            region.ApplyState(unselectedFillAlpha, unselectedOutlineAlpha, unselectedOutlineWidth);
            region.transform.localScale = unselectedScale;
        }

        // select this region
        ApplyState(selectedFillAlpha, selectedOutlineAlpha, selectedOutlineWidth);
        transform.localScale = selectedScale;
        currentlySelected = this;

        Debug.Log($"➡️ Loading scene: {sceneToLoad}");
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// Applies fill alpha, outline alpha & width to every county in this region.
    /// </summary>
    private void ApplyState(float fillA, float outlineA, float outlineW)
    {
        foreach (var rend in countyRenderers)
        {
            if (!regionData.TryGetValue(rend.gameObject.name, out var baseColor))
                continue;

            // fill
            var f = baseColor;
            f.a = fillA;
            rend.color = f;

            // outline (clone material each time)
            var mat = new Material(outlineMaterial);
            var o = baseColor;
            o.a = outlineA;
            mat.SetColor("_OutlineColor", o);
            mat.SetFloat("_OutlineWidth", outlineW);
            rend.material = mat;
        }
    }
}
