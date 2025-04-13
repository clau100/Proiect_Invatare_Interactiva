using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SaveAndCloseAllScenes
{
    [MenuItem("Tools/Save and Close All Scenes")]
    public static void SaveAndCloseScenes()
    {
        // Get all open scenes
        int sceneCount = EditorSceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            var scene = EditorSceneManager.GetSceneAt(i);

            // Save the scene if it has been modified
            if (scene.isDirty)
            {
                bool saved = EditorSceneManager.SaveScene(scene);
                if (saved)
                {
                    Debug.Log($"✅ Saved scene: {scene.path}");
                }
                else
                {
                    Debug.LogError($"❌ Failed to save scene: {scene.path}");
                }
            }
        }

        // Close all scenes
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Debug.Log("🎉 All scenes saved and closed!");
    }
}
