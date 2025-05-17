using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA; // set in inspector, only used to seed
    private List<QuestionAndAnswers> questions; // runtime list

    public GameObject[] options;
    public int currentQuestion;
    public TextMeshProUGUI feedbackText;


    public TextMeshProUGUI QuestionTxt;

    // Quiz completion UI
    public GameObject quizCompletePanel;
    public TextMeshProUGUI scoreText;

    // Tracking score
    private int totalQuestions = 0;

    // PuzzleNavigation
    public string PuzzleName;

    private int lastQuestion = -1;
    public List<AnswerScript> answerButtons;
    private bool isFirstQuestion = true;

    private void Start()
    {
        if (GameManager.Instance.currentQnA == null || GameManager.Instance.currentQnA.Count == 0)
        {
            GameManager.Instance.currentQnA = new List<QuestionAndAnswers>(QnA);
        }

        questions = GameManager.Instance.currentQnA;

        if (totalQuestions == 0) 
        {
            totalQuestions = QnA.Count; 
        }

        if (GameManager.Instance.puzzleCompleted)
        {
            ShowResults();
        }
        else
        {
            generateQuestion();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            AnswerScript answerScript = options[i].GetComponent<AnswerScript>();

            answerScript.quizManager = this;
            answerScript.isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                questions[currentQuestion].Answers[i];

            if (questions[currentQuestion].CorrectAnswer == i)
            {
                answerScript.isCorrect = true;
            }
        }
    }

    public void AnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            GameManager.Instance.correctAnswers++;
            GameManager.Instance.totalAttempts++;
            GameManager.Instance.returnToQuiz = true;
            questions.RemoveAt(currentQuestion);
            SceneManager.LoadScene(this.PuzzleName);
        }
        else
        {
            StartCoroutine(WaitBeforeNextQuestion()); // Delay so we can color the button red
            GameManager.Instance.totalAttempts++;
        }
    }

    private IEnumerator WaitBeforeNextQuestion()
    {
        yield return new WaitForSeconds(0.7f); // Wait 0.7 second
        generateQuestion();
    }



    void generateQuestion()
    {
        // Only reset colors if it's not the first question
        if (!isFirstQuestion)
        {
            foreach (var btn in answerButtons)
            {
                btn.ResetAnswer();
            }
        }

        if (questions.Count == 0) return;

        int newQuestion;
        do
        {
            newQuestion = Random.Range(0, questions.Count);
        } while (questions.Count > 1 && newQuestion == lastQuestion);

        currentQuestion = newQuestion;
        lastQuestion = currentQuestion;

        QuestionTxt.text = questions[currentQuestion].Question;
        SetAnswers();

        // After the first question is loaded, flag it
        isFirstQuestion = false;
    }


    // Display the quiz result
    void ShowResults()
    {
        int correct = GameManager.Instance.correctAnswers;
        int attempts = GameManager.Instance.totalAttempts;

        quizCompletePanel.SetActive(true);
        float percentage = (float)correct / attempts;
        scoreText.text = "Raspunsuri corecte: " + correct + " din " + attempts + " incercari.";

        // Default color
        Color bgColor = Color.white;

        if (percentage < 0.3f)
        {
            feedbackText.text = "JALE, mai invata puiu!";
            bgColor = new Color(0.8f, 0f, 0f); // Red
        }
        else if (percentage > 0.3f && percentage < 0.8f)
        {
            feedbackText.text = "Ești bine moșule!";
            bgColor = new Color(1f, 0.65f, 0f); // Orange-yellow
        }
        else if (percentage >= 0.9f)
        {
            feedbackText.text = "ESTI TOP MODEL!";
            bgColor = new Color(0f, 0.7f, 0.1f); // Green
        }

        // Apply background color
        quizCompletePanel.GetComponent<Image>().color = bgColor;
        GameManager.Instance.completedPuzzles.Add(this.PuzzleName);
        ResetQuiz();
    }

    public void ResetQuiz()
    {
        totalQuestions = 0;
        lastQuestion = -1;
        isFirstQuestion = true;
    }
}
