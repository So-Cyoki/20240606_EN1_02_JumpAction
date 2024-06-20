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
    public float powerValueMax;
    //[HideInInspector]
    public float powerValue;
    public float consumPowerValue;
    public float addPowerValue;
    public float addTime;
    float currentTime;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        powerValue = powerValueMax;
    }

    private void Update()
    {
        //powerValue seconds Add
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = addTime;
            if (powerValue < powerValueMax)
                powerValue += addPowerValue;
            else
                powerValue = powerValueMax;
        }
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
        if (isAddForce && powerValue > 0)
        {
            powerValue -= consumPowerValue;
            if (powerValue < 0)
                powerValue = 0;
            float mouseLength = (mouseStartPos - mouseEndPos).magnitude;
            rig.AddForce(forceValue * mouseLength * Time.deltaTime * mouseDir, ForceMode.Impulse);
            isAddForce = false;
        }
    }


}
