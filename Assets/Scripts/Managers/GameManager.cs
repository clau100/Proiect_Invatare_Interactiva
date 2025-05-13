using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int correctAnswers = 0;
    public int totalAttempts = 0;
    public bool returnToQuiz = false;
    public bool puzzleCompleted = false;
    public HashSet<int> placedPieces = new HashSet<int>();
    public List<QuestionAndAnswers> currentQnA; // stores the quiz list between scenes

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

    public void ResetGame()
    {
        correctAnswers = 0;
        returnToQuiz = false;
        placedPieces.Clear(); 
    }
}
