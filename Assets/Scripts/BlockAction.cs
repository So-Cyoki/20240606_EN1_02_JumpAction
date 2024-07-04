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
    Renderer ballRenderer;
    public Color startColor;
    public Color endColor;
    public float color_targetPosZ;//到达什么位置之后变为最终颜色
    float color_moveLength;//一开始的距离是多少，用于判断移动到什么位置变化成什么颜色

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ballRenderer = GetComponent<Renderer>();
        //startColor = renderer.material.color;
        color_moveLength = Mathf.Abs(transform.position.z - color_targetPosZ);
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
        Color colorLerp = Color.Lerp(startColor, endColor,
        Mathf.Abs((color_moveLength - transform.position.z) / color_moveLength));
        ballRenderer.material.color = colorLerp;
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
    private void OnTriggerEnter(Collider other)
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
