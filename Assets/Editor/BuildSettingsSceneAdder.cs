using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class BuildSettingsSceneAdder : MonoBehaviour
{
    [MenuItem("Tools/Add All Scenes To Build Settings")]
    static void AddAllScenesToBuildSettings()
    {
        string[] sceneGUIDs = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });

        List<EditorBuildSettingsScene> newScenes = new List<EditorBuildSettingsScene>();

        foreach (string guid in sceneGUIDs)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);

            // Prevent duplicates
            bool alreadyInBuild = false;
            foreach (var existingScene in EditorBuildSettings.scenes)
            {
                if (existingScene.path == scenePath)
                {
                    alreadyInBuild = true;
                    break;
                }
            }

            if (!alreadyInBuild)
            {
                newScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                Debug.Log($"✅ Added to build settings: {scenePath}");
            }
        }

        // Append new scenes to existing ones
        var finalSceneList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        finalSceneList.AddRange(newScenes);

        EditorBuildSettings.scenes = finalSceneList.ToArray();

        Debug.Log($"🎉 {newScenes.Count} scenes added to Build Settings!");
    }
}
