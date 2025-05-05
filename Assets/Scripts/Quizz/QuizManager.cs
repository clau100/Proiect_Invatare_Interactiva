using UnityEngine;

// Assets/Scripts/Quiz/QuizManager.cs

using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string text;
        public string[] choices;
        public int correctIndex;
    }

    public Question[] questions;
    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;
    public GameObject quizPanel;
    public GameObject digGridContainer;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        digGridContainer.SetActive(false);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        var q = questions[currentQuestion];
        questionText.text = q.text;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < q.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.choices[i];
                int idx = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnAnswer(idx));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnAnswer(int chosen)
    {
        if (chosen == questions[currentQuestion].correctIndex)
            score++;

        currentQuestion++;
        if (currentQuestion < questions.Length)
            ShowQuestion();
        else
            EndQuiz();
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        digGridContainer.SetActive(true);
    }
}

