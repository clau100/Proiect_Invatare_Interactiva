using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int correctAnswers = 0;
    public int totalAttempts = 0;
    public bool returnToQuiz = false;
    public bool puzzleCompleted = false;
    public HashSet<int> placedPieces = new HashSet<int>();
    public List<QuestionAndAnswers> currentQnA; // stores the quiz list between scenes

    public List<string> completedPuzzles = new List<string>();
    public int infoIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void ResetGame()
    {
        correctAnswers = 0;
        totalAttempts = 0;
        returnToQuiz = false;
        puzzleCompleted = false;
        placedPieces.Clear();
        currentQnA = new List<QuestionAndAnswers>();
        SceneManager.LoadScene("StartScene");
    }

    public void LoadInfo()
    {
        SceneManager.LoadScene("InfoScene");
    }
}
