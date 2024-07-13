using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MouseMove player;
    public GameObject ui_arrow;
    public GameObject ui_gameStart;
    public GameObject ui_gameOver;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ui_gameStart.SetActive(true);
        ui_gameOver.SetActive(false);
        ui_arrow.SetActive(false);
    }

    private void Update()
    {
        if (player.isDead)
        {
            ui_gameOver.SetActive(true);
        }
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void ShowArrow(bool flag)
    {
        if (flag)
            ui_arrow.SetActive(true);
        else
            ui_arrow.SetActive(false);
    }
}
