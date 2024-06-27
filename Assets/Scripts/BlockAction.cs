using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BlockAction : MonoBehaviour
{
    public float speed;
    public bool isStaty;//是否到位了
    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Move
        if (transform.position.z > 0)
        {
            //transform.position -= new Vector3(0, 0, speed);
            rig.AddForce(Vector3.back * speed);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            isStaty = true;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (isStaty)
        {
            if (other.transform.CompareTag("Block"))
                Destroy(other.gameObject);
            else if (other.transform.CompareTag("Player"))
                Destroy(this.gameObject);
        }
    }
}
