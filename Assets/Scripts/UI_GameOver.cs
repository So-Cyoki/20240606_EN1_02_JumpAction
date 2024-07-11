using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public UI_ScoreText uI_ScoreText;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textHighScore;
    int highScore;

    private void Update()
    {
        if (highScore < uI_ScoreText.score)
            highScore = uI_ScoreText.score;
        textScore.text = "Score\n" + uI_ScoreText.score;
        textHighScore.text = "HighScore\n" + highScore;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
