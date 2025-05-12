using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TextMeshProUGUI feedbackText;


    public TextMeshProUGUI QuestionTxt;

    // NEW: Quiz completion UI
    public GameObject quizCompletePanel;
    public TextMeshProUGUI scoreText;

    // NEW: Tracking score
    private int totalQuestions;
    private int correctAnswers;

    private void Start()
    {
        totalQuestions = QnA.Count;
        generateQuestion();
    }

    //public void correct()
    //{
    //    correctAnswers++;
    //    QnA.RemoveAt(currentQuestion);
    //    Debug.Log("Remaining questions: " + QnA.Count);

    //    if (QnA.Count > 0)
    //    {
    //        generateQuestion();
    //    }
    //    else
    //    {
    //        ShowResults();
    //    }
    //}


    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            AnswerScript answerScript = options[i].GetComponent<AnswerScript>();

            answerScript.quizManager = this;
            answerScript.isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i)
            {
                answerScript.isCorrect = true;
            }
        }
    }

    public void AnswerSelected(bool isCorrect)
    {
        if (isCorrect)
            correctAnswers++;

        QnA.RemoveAt(currentQuestion);

        if (QnA.Count > 0)
        {
            generateQuestion();
        }
        else
        {
            ShowResults();
        }
    }


    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
    }

    // NEW: Display the quiz result
    void ShowResults()
    {
        Debug.Log("ShowResults() called");

        quizCompletePanel.SetActive(true);

        float percentage = (float)correctAnswers / totalQuestions;
        scoreText.text = "Raspunsuri corecte: " + correctAnswers + " / " + totalQuestions;

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
            feedbackText.text = "ESTI TOP!";
            bgColor = new Color(0f, 0.7f, 0.1f); // Green
        }

        // Apply background color
        quizCompletePanel.GetComponent<Image>().color = bgColor;
    }
}
