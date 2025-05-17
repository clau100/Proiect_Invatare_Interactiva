using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    private int totalButtons = 9;
    private int buttonsPerRow = 3;
    private Vector2 buttonSize = new Vector2(500, 200);
    private Vector2 spacing = new Vector2(10, 10);

    private List<string> regions = new List<string>{
        "Crisana",
        "Dobrogea",
        "Banat",
        "Bucovina",
        "Moldova",
        "Maramures",
        "Oltenia",
        "Muntenia",
        "Transilvania"
    };

    private GUIStyle buttonStyle;
    private void Start()
    {
        // Initialize button style
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 18;                // Bigger font size
        buttonStyle.normal.textColor = Color.black; // Black text
        buttonStyle.hover.textColor = Color.black;  // Keep black on hover
        buttonStyle.active.textColor = Color.black; // Keep black on click
    }
    public void OnStartGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null!");
            return;
        }

        Debug.Log("Calling GameManager.StartGame()");
        GameManager.Instance.ResetGame();
    }

    private void OnGUI()
    {
        // Calculate total width and height of the grid
        float gridWidth = buttonsPerRow * buttonSize.x + (buttonsPerRow - 1) * spacing.x;
        int rows = totalButtons / buttonsPerRow;
        float gridHeight = rows * buttonSize.y + (rows - 1) * spacing.y;

        // Calculate top-left position to center the grid on screen
        float startX = (Screen.width - gridWidth) / 2f;
        float startY = (Screen.height - gridHeight) / 2f;

        GUI.BeginGroup(new Rect(startX, startY, gridWidth, gridHeight));

        GUIStyle customButtonStyle = new GUIStyle(GUI.skin.button);
        customButtonStyle.fontSize = 80; // change font size
        customButtonStyle.normal.textColor = Color.black; // change text color

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < buttonsPerRow; col++)
            {
                int buttonIndex = row * buttonsPerRow + col;
                Rect buttonRect = new Rect(
                    col * (buttonSize.x + spacing.x),
                    row * (buttonSize.y + spacing.y),
                    buttonSize.x,
                    buttonSize.y
                );

                bool isClickable = false;
                foreach (string region in GameManager.Instance.completedPuzzles)
                    if (region.Contains(regions[buttonIndex]))
                        isClickable = true;

                GUI.enabled = isClickable;

                if (GUI.Button(buttonRect, regions[buttonIndex], customButtonStyle))
                {
                    GameManager.Instance.infoIndex = buttonIndex;
                    SceneManager.LoadScene("LocationInfoScene");
                    Debug.Log("Button " + (buttonIndex + 1) + " clicked!");
                }

                GUI.enabled = true; // reset
            }
        }

        GUI.EndGroup();
    }
}
