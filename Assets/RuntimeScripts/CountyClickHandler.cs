using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CountyClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Material outlineMaterial;

    static readonly Dictionary<string, Color> countyData = new Dictionary<string, Color>
    {
        { "Satu Mare",          new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Arad",               new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Bihor",              new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Timis",              new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Mehedinti",          new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Dolj",               new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Calarasi",           new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Teleorman",          new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Giurgiu",            new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Constanta",          new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Olt",                new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Caras-Severin",      new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Botosani",           new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Iasi",               new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Vaslui",             new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Galati",             new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Suceava",            new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Maramures",          new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Tulcea",             new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Cluj",               new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Bistrita-Nasaud",    new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Salaj",              new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Dâmbovita",          new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Ilfov",              new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Arges",              new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Gorj",               new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Hunedoara",          new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Vâlcea",             new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Prahova",            new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Covasna",            new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Vrancea",            new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Buzau",              new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Brasov",             new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Sibiu",              new Color(25.0f / 255.0f, 165.0f / 255.0f, 240.0f / 255.0f, 1.0f)},
        { "Mures",              new Color(250.0f / 255.0f, 165.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Harghita",           new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Neamt",              new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
        { "Bacau",              new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Alba",               new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Braila",             new Color(241.0f / 255.0f, 95.0f / 255.0f, 46.0f / 255.0f, 1.0f)},
        { "Ialomita",           new Color(107.0f / 255.0f, 101.0f / 255.0f, 211.0f / 255.0f, 1.0f)},
        { "Bucharest",          new Color(105.0f / 255.0f, 206.0f / 255.0f, 60.0f / 255.0f, 1.0f)},
    };

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material = new Material(outlineMaterial);

        Color unfocusedColor = countyData[gameObject.name];
        unfocusedColor.a = 0.3f;
        spriteRenderer.material.SetColor("_OutlineColor", unfocusedColor);
    }

    private void OnMouseEnter()
    {
        spriteRenderer.material.SetColor("_OutlineColor", countyData[gameObject.name]);

        TMP_Text countryUIText = GameObject.Find("CountryUI").GetComponent<TMP_Text>();
        countryUIText.text = gameObject.name;
    }

    private void OnMouseExit()
    {
        Color unfocusedColor = countyData[gameObject.name];
        unfocusedColor.a = 0.3f;

        spriteRenderer.material.SetColor("_OutlineColor", unfocusedColor);

        TMP_Text countryUIText = GameObject.Find("CountryUI").GetComponent<TMP_Text>();
        countryUIText.text = "";
    }

    private void OnMouseDown()
    {
        string sceneName = gameObject.name;
        Debug.Log($"➡️ Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
