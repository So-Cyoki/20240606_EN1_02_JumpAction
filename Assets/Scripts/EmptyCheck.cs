using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool isBlockOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Block"))
            isBlockOn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Block"))
            isBlockOn = false;
    }
}
