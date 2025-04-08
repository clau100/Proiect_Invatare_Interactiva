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

            string countyName = sprite.name;

            // Create new scene
            var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);

            // Create Canvas
            GameObject canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObject.AddComponent<GraphicRaycaster>();

            // Create Button
            GameObject buttonObject = new GameObject("CountyButton");
            buttonObject.transform.SetParent(canvasObject.transform, false);
            var button = buttonObject.AddComponent<Button>();

            var image = buttonObject.AddComponent<Image>();
            image.sprite = sprite;

            // Resize button to sprite size
            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = sprite.rect.size;

            // Add click listener to load this scene
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(countyName);
            });

            // Add event system if needed
            if (Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            // Save the scene
            string scenePath = $"{scenesFolder}/{countyName}.unity";
            EditorSceneManager.SaveScene(newScene, scenePath);
            Debug.Log($"✅ Scene created: {scenePath}");
        }

        AssetDatabase.Refresh();
        Debug.Log("🎉 All county scenes generated!");
    }
}
