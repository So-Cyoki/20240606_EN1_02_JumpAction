using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameStart : MonoBehaviour
{
    float originalTimeScale;
    float originalFixDeltaTime;
    float timeScaleValue;
    private void Start()
    {
        timeScaleValue = 0;
    }

    private void Update()
    {
        Time.timeScale = originalTimeScale * timeScaleValue;
        Time.fixedDeltaTime = originalFixDeltaTime * timeScaleValue;
    }

    public void CloseUI()
    {
        timeScaleValue = 1;
        gameObject.SetActive(false);
    }
}
