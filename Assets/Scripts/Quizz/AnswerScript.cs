using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager not assigned!");
            return;
        }

        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.AnswerSelected(true);
        }
        else
        {
            Debug.Log("Wrong Answer");
            quizManager.AnswerSelected(false);
        }
    }
}
