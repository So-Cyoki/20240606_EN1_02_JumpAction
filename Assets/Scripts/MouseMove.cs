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
    public float powerValueMax;//能量的上限
    [HideInInspector]
    public float powerValue;
    public float consumPowerValue;//消耗多少能量
    public float addPowerValue;//回复多少能量
    public float addTime;//每次回复能量的时间
    float currentTime;
    public float timeScaleValue;
    float originalFixDeltaTime;//保存原本的物理计算时间
    float originalTimeScale;//保存原本的帧数时间

    bool isDead;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        powerValue = powerValueMax;
    }

    private void Start()
    {
        originalFixDeltaTime = Time.fixedDeltaTime;
        originalTimeScale = Time.timeScale;
    }

    private void Update()
    {
        //随着时间回复能量
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
            Time.timeScale = originalTimeScale * timeScaleValue;
            Time.fixedDeltaTime = originalFixDeltaTime * timeScaleValue;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAddForce = true;
            rig.velocity = Vector3.zero;//发射的时候把速度清零，会更有操作感(为了对抗下落的速度)
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = originalFixDeltaTime;
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

        //isDead
        if (isDead)
        {
            rig.constraints &= ~RigidbodyConstraints.FreezePositionZ;//解锁Z轴
            rig.velocity = Vector3.zero;
            rig.AddForce(Vector3.back * 10, ForceMode.Impulse);
            isDead = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Block"))
        {
            isDead = true;
        }
    }
}
