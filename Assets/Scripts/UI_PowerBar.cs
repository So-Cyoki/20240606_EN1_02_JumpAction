using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerBar : MonoBehaviour
{
    public RectTransform powerBar;
    public GameObject player;
    MouseMove mouseMoveScript;
    public float speed;
    float currentVaule;
    float changedValue;
    float barLength;
    float powerValueLength;

    private void Awake()
    {
        mouseMoveScript = player.GetComponent<MouseMove>();
    }
    private void Start()
    {
        barLength = powerBar.sizeDelta.x;
        powerValueLength = mouseMoveScript.powerValue;
        currentVaule = mouseMoveScript.powerValue;
        changedValue = currentVaule;
    }

    private void Update()
    {
        changedValue = mouseMoveScript.powerValue;
        if (changedValue > currentVaule)
            currentVaule += speed;
        else
            currentVaule = changedValue;

        powerBar.sizeDelta = new(currentVaule / powerValueLength * barLength, powerBar.sizeDelta.y);
    }
}
