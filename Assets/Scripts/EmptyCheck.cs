using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool isBlockOn;
    UI_ScoreText ui_ScoreText;
    MouseMove mouseMove;
    private void Start()
    {
        ui_ScoreText = transform.parent.parent.GetComponent<RuleManager>().ui_ScoreText;
        mouseMove = transform.parent.parent.GetComponent<RuleManager>().mouseMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Block"))
        {
            isBlockOn = true;
            if (!mouseMove.isDead)
                ui_ScoreText.AddScore();
        }
        else
        {
            isBlockOn = false;
        }
    }
}
