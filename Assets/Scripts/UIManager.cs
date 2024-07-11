using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MouseMove player;
    public GameObject ui_gameStart;
    public GameObject ui_gameOver;

    private void Start()
    {
        ui_gameStart.SetActive(true);
        ui_gameOver.SetActive(false);
    }

    private void Update()
    {
        if (player.isDead)
        {
            ui_gameOver.SetActive(true);
        }
    }
}
