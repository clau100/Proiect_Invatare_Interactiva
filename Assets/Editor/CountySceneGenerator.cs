using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class CountySceneGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate County Scenes")]
    static void GenerateScenes()
    {
        string svgFolder = "Assets/svg";
        string scenesFolder = "Assets/Scenes/Counties";

        // Ensure folder exists
        if (!AssetDatabase.IsValidFolder(scenesFolder))
        {
            AssetDatabase.CreateFolder("Assets/Scenes", "Counties");
        }

        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { svgFolder });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite == null) continue;

            // Remove "Sprite" suffix from the name if it exists
            string countyName = sprite.name;
            if (countyName.EndsWith("Sprite"))
            {
                countyName = countyName.Substring(0, countyName.Length - "Sprite".Length);
            }

            // Create new scene
            var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);

            // Create Canvas
            GameObject canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace; // Set to World Space
            canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObject.AddComponent<GraphicRaycaster>();

            RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(1920, 1080); // Set canvas size
            canvasObject.transform.position = Vector3.zero; // Center canvas at (0, 0, 0)
            canvasObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); // Scale down the canvas to fit the world space

            // Create 10x10 Grid of Buttons
            GameObject gridObject = new GameObject("TileGrid");
            gridObject.transform.SetParent(canvasObject.transform, false);
            gridObject.transform.localScale = new Vector3(2, 2, 2); // Apply scale of 2, 2, 2 to the grid
            GridLayoutGroup gridLayout = gridObject.AddComponent<GridLayoutGroup>();
            RectTransform gridRect = gridObject.GetComponent<RectTransform>();

            // Configure RectTransform to center the grid and its tiles
            gridRect.anchorMin = new Vector2(0.5f, 0.5f);
            gridRect.anchorMax = new Vector2(0.5f, 0.5f);
            gridRect.pivot = new Vector2(0.5f, 0.5f);
            gridRect.sizeDelta = new Vector2(600, 540); // Adjust grid size
            gridRect.anchoredPosition3D = Vector3.zero; // Center the grid at (0, 0, 0)

            // Set GridLayoutGroup alignment to center the tiles
            gridLayout.childAlignment = TextAnchor.MiddleCenter;

            gridLayout.cellSize = new Vector2(30, 30); // Smaller size for each tile
            gridLayout.spacing = new Vector2(5, 5); // Spacing between tiles
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = 10; // 10 columns


            for (int i = 0; i < 100; i++) // 10x10 grid = 100 tiles
            {
                GameObject buttonObject = new GameObject($"Tile_{i}");
                buttonObject.transform.SetParent(gridObject.transform, false);
                Button button = buttonObject.AddComponent<Button>();
                Image buttonImage = buttonObject.AddComponent<Image>();
                buttonImage.color = Color.gray; // Default tile color

                // Add click listener to reveal hidden object
                int tileIndex = i; // Capture index for the lambda
                button.onClick.AddListener(() =>
                {
                    Debug.Log($"Tile {tileIndex} clicked!");
                    RevealTile(buttonObject);
                });
            }

            // Add event system if needed
            if (Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            // Add Title
            GameObject titleObject = new GameObject("Title");
            titleObject.transform.SetParent(canvasObject.transform, false);
            Text titleText = titleObject.AddComponent<Text>();
            titleText.text = countyName; // Use the county name as the title
            titleText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.color = Color.white;
            titleText.fontSize = 52; // Set font size to 3x larger

            RectTransform titleRect = titleObject.GetComponent<RectTransform>();
            titleRect.sizeDelta = new Vector2(400, 100);
            titleRect.anchoredPosition = new Vector2(0, 400);

            // Add Camera to Scene
            GameObject cameraObject = new GameObject("MainCamera");
            Camera camera = cameraObject.AddComponent<Camera>();
            cameraObject.tag = "MainCamera";
            camera.transform.position = new Vector3(0, 0, -20); // Position the camera to view the grid
            camera.clearFlags = CameraClearFlags.Skybox; // Set the background type to Skybox

            // Set the camera to orthographic projection
            camera.orthographic = true;
            camera.orthographicSize = 10; // Adjust the size to control the visible area

            // Optionally assign a skybox material if one exists
            Material skyboxMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/DefaultSkybox.mat");
            if (skyboxMaterial != null)
            {
                RenderSettings.skybox = skyboxMaterial;
            }
            else
            {
                Debug.LogWarning("No skybox material found. Please assign one manually.");
            }



            // Save the scene
            string scenePath = $"{scenesFolder}/{countyName}.unity";
            EditorSceneManager.SaveScene(newScene, scenePath);
            Debug.Log($"✅ Scene created: {scenePath}");
        }

        AssetDatabase.Refresh();
        Debug.Log("🎉 All county scenes generated!");
    }

    static void RevealTile(GameObject tile)
    {
        // Logic to reveal hidden object behind the tile
        Image tileImage = tile.GetComponent<Image>();
        tileImage.color = Color.green; // Change color to indicate revealed
        Debug.Log($"Revealed tile: {tile.name}");
    }
}
