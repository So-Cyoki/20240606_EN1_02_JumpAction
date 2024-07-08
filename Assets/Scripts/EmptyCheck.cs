using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool isBlockOn;
    UI_ScoreText ui_ScoreText;
    private void Start()
    {
        ui_ScoreText = transform.parent.parent.GetComponent<RuleManager>().ui_ScoreText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Block"))
        {
            isBlockOn = true;
            ui_ScoreText.AddScore();
        }
        else
        {
            isBlockOn = false;
        }
    }
}
