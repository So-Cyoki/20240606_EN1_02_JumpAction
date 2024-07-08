using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScoreText : MonoBehaviour
{
    public int score;
    TextMeshProUGUI text;
    readonly string title = "SCORE: ";

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        text.text = title + score;
    }

    public void AddScore()
    {
        score++;
    }
}
