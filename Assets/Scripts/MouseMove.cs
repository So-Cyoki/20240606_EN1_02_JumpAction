using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    private Rigidbody rig;
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;
    private Vector3 mouseDir;
    public bool isAddForce;
    public float forceValue;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //MousePosition
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAddForce = true;
        }
        mouseDir = (mouseStartPos - mouseEndPos).normalized;
        //AddForce
        if (isAddForce)
        {
            float mouseLength = (mouseStartPos - mouseEndPos).magnitude * 0.01f;
            rig.AddForce(forceValue * mouseLength * mouseDir, ForceMode.Impulse);
            isAddForce = false;
        }
    }
}
