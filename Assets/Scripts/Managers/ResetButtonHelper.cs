using UnityEngine;
using UnityEngine.UI;

public class ResetButtonHelper : MonoBehaviour
{
    public void CallResetGame()
    {
        GameManager.Instance?.ResetGame();
    }
}
