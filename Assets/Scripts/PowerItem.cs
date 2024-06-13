using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DestroyMy()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            animator.SetTrigger("tGet");
        }
    }
}
