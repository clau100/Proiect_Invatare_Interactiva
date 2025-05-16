using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    private Button button;
    private Image buttonImage;

    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;
    private Color defaultColor;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }

    public void Answer()
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager not assigned!");
            return;
        }

        foreach (var btn in quizManager.answerButtons)
        {
            btn.GetComponent<Button>().interactable = false;
        }

        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            SetButtonColor(correctColor);
            quizManager.AnswerSelected(true);
        }
        else
        {
            Debug.Log("Wrong Answer");
            SetButtonColor(wrongColor);
            quizManager.AnswerSelected(false);
        }
    }

    private void SetButtonColor(Color color)
    {
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }

    public void ResetAnswer()
    {
        if (button == null)
            button = GetComponent<Button>();
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();

        button.interactable = true;
        ColorUtility.TryParseHtmlString("#3F3A3A", out defaultColor);
        buttonImage.color = defaultColor;
    }
}
