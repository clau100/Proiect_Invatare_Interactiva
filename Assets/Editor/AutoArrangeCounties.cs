using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AutoArrangeCountiesFromSVG : MonoBehaviour
{
    struct CountyData
    {
        public Vector2 position;
        public Vector2 size;

        public CountyData(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }
    }

    static readonly Dictionary<string, CountyData> countyData = new Dictionary<string, CountyData>
    {
        { "Satu Mare", new CountyData(new Vector2(300.80f, 886.50f), new Vector2(140.60f, 112.00f)) },
        { "Arad", new CountyData(new Vector2(188.40f, 684.85f), new Vector2(197.20f, 106.50f)) },
        { "Bihor", new CountyData(new Vector2(225.50f, 783.85f), new Vector2(133.00f, 173.10f)) },
        { "Timis", new CountyData(new Vector2(156.05f, 600.35f), new Vector2(221.10f, 140.50f)) },
        { "Mehedinti", new CountyData(new Vector2(287.65f, 454.90f), new Vector2(139.90f, 134.00f)) },
        { "Dolj", new CountyData(new Vector2(365.50f, 407.55f), new Vector2(138.80f, 131.50f)) },
        { "Calarasi", new CountyData(new Vector2(709.60f, 416.55f), new Vector2(167.60f, 67.10f)) },
        { "Teleorman", new CountyData(new Vector2(520.25f, 386.75f), new Vector2(105.90f, 117.70f)) },
        { "Giurgiu", new CountyData(new Vector2(596.75f, 393.75f), new Vector2(93.10f, 113.50f)) },
        { "Constanta", new CountyData(new Vector2(802.70f, 409.50f), new Vector2(166.80f, 140.60f)) },
        { "Olt", new CountyData(new Vector2(443.00f, 419.60f), new Vector2(91.00f, 156.00f)) },
        { "Caras-Severin", new CountyData(new Vector2(216.75f, 527.25f), new Vector2(131.10f, 145.30f)) },
        { "Botosani", new CountyData(new Vector2(672.65f, 907.25f), new Vector2(127.70f, 121.50f)) },
        { "Iasi", new CountyData(new Vector2(724.20f, 813.85f), new Vector2(157.80f, 106.90f)) },
        { "Vaslui", new CountyData(new Vector2(765.00f, 714.15f), new Vector2(100.60f, 136.50f)) },
        { "Galati", new CountyData(new Vector2(765.45f, 613.65f), new Vector2(92.70f, 100.90f)) },
        { "Suceava", new CountyData(new Vector2(580.50f, 859.05f), new Vector2(166.60f, 132.50f)) },
        { "Maramures", new CountyData(new Vector2(406.55f, 879.20f), new Vector2(198.70f, 100.00f)) },
        { "Tulcea", new CountyData(new Vector2(872.20f, 518.45f), new Vector2(164.60f, 105.10f)) },
        { "Cluj", new CountyData(new Vector2(354.00f, 767.65f), new Vector2(151.20f, 131.10f)) },
        { "Bistrita-Nasaud", new CountyData(new Vector2(456.00f, 809.45f), new Vector2(111.00f, 118.50f)) },
        { "Salaj", new CountyData(new Vector2(326.05f, 806.60f), new Vector2(128.30f, 77.00f)) },
        { "Dâmbovita", new CountyData(new Vector2(560.05f, 501.65f), new Vector2(83.30f, 138.70f)) },
        { "Ilfov", new CountyData(new Vector2(615.70f, 443.45f), new Vector2(55.00f, 68.70f)) },
        { "Arges", new CountyData(new Vector2(491.80f, 509.10f), new Vector2(86.20f, 165.20f)) },
        { "Gorj", new CountyData(new Vector2(330.05f, 502.90f), new Vector2(123.10f, 109.00f)) },
        { "Hunedoara", new CountyData(new Vector2(308.35f, 617.40f), new Vector2(119.70f, 150.80f)) },
        { "Vâlcea", new CountyData(new Vector2(412.40f, 515.05f), new Vector2(92.20f, 142.30f)) },
        { "Prahova", new CountyData(new Vector2(600.55f, 526.55f), new Vector2(112.90f, 105.50f)) },
        { "Covasna", new CountyData(new Vector2(595.70f, 636.75f), new Vector2(99.60f, 100.70f)) },
        { "Vrancea", new CountyData(new Vector2(693.15f, 616.20f), new Vector2(115.30f, 108.80f)) },
        { "Buzau", new CountyData(new Vector2(669.50f, 547.95f), new Vector2(132.40f, 142.50f)) },
        { "Brasov", new CountyData(new Vector2(538.85f, 615.95f), new Vector2(139.50f, 107.30f)) },
        { "Sibiu", new CountyData(new Vector2(435.05f, 627.10f), new Vector2(131.90f, 112.60f)) },
        { "Mures", new CountyData(new Vector2(467.05f, 727.95f), new Vector2(127.10f, 145.30f)) },
        { "Harghita", new CountyData(new Vector2(559.05f, 733.50f), new Vector2(137.50f, 144.80f)) },
        { "Neamt", new CountyData(new Vector2(642.70f, 781.05f), new Vector2(150.80f, 94.10f)) },
        { "Bacau", new CountyData(new Vector2(672.35f, 703.80f), new Vector2(145.30f, 112.20f)) },
        { "Alba", new CountyData(new Vector2(353.00f, 647.80f), new Vector2(145.20f, 155.00f)) },
        { "Braila", new CountyData(new Vector2(754.70f, 526.50f), new Vector2(107.20f, 100.20f)) },
        { "Ialomita", new CountyData(new Vector2(713.30f, 456.50f), new Vector2(177.00f, 69.00f)) },
        { "Bucharest", new CountyData(new Vector2(607.90f, 436.15f), new Vector2(22.60f, 35.90f)) }
    };

    [MenuItem("Tools/Arrange Counties Into Map")]
    static void ArrangeCounties()
    {
        string assetFolder = "Assets/svg";
        GameObject parent = new GameObject("Romania Map");

        foreach (var county in countyData)
        {
            string countyName = county.Key;
            CountyData data = county.Value;

            string assetPath = $"{assetFolder}/{countyName}.svg";
            var assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            Sprite sprite = null;

            foreach (var asset in assets)
            {
                if (asset is Sprite candidateSprite)
                {
                    sprite = candidateSprite;
                    break;
                }
            }

            if (sprite == null)
            {
                Debug.LogWarning($"Sprite not found inside: {assetPath}.");
                continue;
            }

            GameObject countyObject = new GameObject(countyName);
            countyObject.transform.parent = parent.transform;

            var renderer = countyObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;

            // Scale county to match original SVG size
            Vector2 spriteSize = sprite.bounds.size;
            Vector3 scaleFactor = Vector3.one;

            if (spriteSize.x != 0 && spriteSize.y != 0)
            {
                scaleFactor = new Vector3(data.size.x / spriteSize.x, data.size.y / spriteSize.y, 1f);
            }

            countyObject.transform.localScale = scaleFactor;

            // Place county at correct position
            Vector2 spriteOffset = Vector2.Scale(sprite.bounds.center, scaleFactor);
            Vector2 finalPosition = data.position - spriteOffset;

            countyObject.transform.position = finalPosition;

            countyObject.AddComponent<PolygonCollider2D>();
        }

        CenterMap(parent);
        // Optional: scale down the entire map to fit Unity world space
        float globalScaleFactor = 0.01f; // Try values like 0.005f, 0.002f, etc.
        parent.transform.localScale = Vector3.one * globalScaleFactor;


        Debug.Log("✅ Counties arranged into map with perfect SVG data!");
    }

    static void CenterMap(GameObject parent)
    {
        if (parent.transform.childCount == 0) return;

        Bounds bounds = new Bounds(parent.transform.GetChild(0).position, Vector3.zero);
        foreach (Transform child in parent.transform)
        {
            bounds.Encapsulate(child.position);
        }

        Vector3 offset = bounds.center;

        foreach (Transform child in parent.transform)
        {
            child.position -= offset;
        }

        parent.transform.position = Vector3.zero;
    }
}
