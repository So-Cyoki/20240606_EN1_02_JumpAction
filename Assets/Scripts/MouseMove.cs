using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    Rigidbody rig;
    Vector3 mouseStartPos;
    Vector3 mouseEndPos;
    Vector3 mouseDir;
    bool isAddForce;
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
            float mouseLength = (mouseStartPos - mouseEndPos).magnitude;
            rig.AddForce(forceValue * mouseLength * Time.deltaTime * mouseDir, ForceMode.Impulse);
            isAddForce = false;
        }
    }
}
