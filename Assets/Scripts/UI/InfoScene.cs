using TMPro;
using UnityEngine;

public class IndexTextUpdater : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI textBox1;
    public TextMeshProUGUI textBox2;

    [Header("Data")]
    public string[] titles;
    public string[] descriptions;
    void Start()
    {
        SetTextByIndex(GameManager.Instance.infoIndex);
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

    public void SetTextByIndex(int index)
    {
        if (index < 0 || index >= titles.Length || index >= descriptions.Length)
        {
            Debug.LogWarning("Invalid index provided.");
            return;
        }

        textBox1.text = titles[index];
        textBox2.text = descriptions[index];
    }
}
