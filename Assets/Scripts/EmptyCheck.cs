using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    public bool isBlockOn;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Block"))
            isBlockOn = true;
        else
            isBlockOn = false;
    }
}
