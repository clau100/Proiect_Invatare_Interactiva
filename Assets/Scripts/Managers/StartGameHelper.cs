using UnityEngine;

public class StartGameHelper : MonoBehaviour
{
    public void OnStartGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null!");
            return;
        }

        Debug.Log("Calling GameManager.StartGame()");
        GameManager.Instance.StartGame();
    }
}
