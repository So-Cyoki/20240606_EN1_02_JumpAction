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
    [HideInInspector]
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
            float force = 0;
            float mouseLength = (mouseStartPos - mouseEndPos).magnitude;
            if (powerValue - consumPowerValue * mouseLength * Time.deltaTime >= 0)
            {
                powerValue -= consumPowerValue * mouseLength * Time.deltaTime;
                force = forceValue * mouseLength * Time.deltaTime;
            }
            else
            {
                //这里有点问题，我想做的效果是最后剩下的能量来决定能弹多远，但是还没写好
                force = forceValue * mouseLength * Time.deltaTime;
                powerValue = 0;
            }
            if (powerValue < 0)
                powerValue = 0;
            rig.AddForce(force * mouseDir, ForceMode.Impulse);
            isAddForce = false;
        }
    }


}
