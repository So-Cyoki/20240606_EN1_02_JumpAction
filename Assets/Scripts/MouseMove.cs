using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class MouseMove : MonoBehaviour
{
    Rigidbody rig;
    MeshRenderer mesh;
    Vector3 mouseStartPos;
    Vector3 mouseEndPos;
    Vector3 mouseDir;
    [Tooltip("发射的力")]
    public float forceValue;
    public float powerValueMax;//能量的上限
    [HideInInspector]
    public float powerValue = 10;
    public float consumPowerValue;//消耗多少能量
    public float consumPowerTime;//每次消耗能量时间
    public float addPowerValue;//回复多少能量
    public float addTime;//每次回复能量的时间
    public float powerRestartValue;//多少能量恢复正常
    float currentTime_addPower;
    float currentTime_consumPower;
    public float timeScaleValue;
    float originalFixDeltaTime;//保存原本的物理计算时间
    float originalTimeScale;//保存原本的帧数时间

    bool isAddForce;
    bool isPowerAllOver;//能量消耗过尽
    bool isTimeSlow;//时间缓慢
    bool isDead;//这个状态还没完全写完！
    bool isDeadEffect;
    public float deadAddForce;
    public Color color;
    public Color noPowerColor;
    public Transform cameraTrans;
    public RectTransform ui_powerValueBar;
    public Image ui_timeSlowImage;
    public float timeSlowImageAlpha;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        powerValue = powerValueMax;
    }

    private void Start()
    {
        originalFixDeltaTime = Time.fixedDeltaTime;
        originalTimeScale = Time.timeScale;
        //获得能量条UI的宽度，然后根据设定的能量恢复状态的数值，来设置低能量提醒UI的宽度
        float powerBarW = ui_powerValueBar.rect.width;
        RectTransform powerMessageBar = ui_powerValueBar.Find("PowerMessage").GetComponent<RectTransform>();
        powerMessageBar.sizeDelta = new Vector2(powerBarW / powerValueMax * powerRestartValue, powerMessageBar.rect.height);
    }

    private void Update()
    {
        //能量消耗过尽
        if (powerValue <= 0)
        {
            isPowerAllOver = true;
            ui_timeSlowImage.DOFade(0, 0.5f);
        }
        else if (powerValue >= powerRestartValue)
            isPowerAllOver = false;
        if (isPowerAllOver)
        {
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = originalFixDeltaTime;
            isAddForce = false;
            isTimeSlow = false;
            mesh.material.color = noPowerColor;
        }
        else
        {
            mesh.material.color = color;
        }
        //随着时间回复能量
        currentTime_addPower -= Time.deltaTime;
        if (currentTime_addPower < 0)
        {
            currentTime_addPower = addTime;
            if (powerValue < powerValueMax)
                powerValue += addPowerValue;
            else
                powerValue = powerValueMax;
        }
        //能量消耗
        if (isTimeSlow)
        {
            currentTime_consumPower -= Time.deltaTime;
            if (currentTime_consumPower < 0)
            {
                currentTime_consumPower = consumPowerTime;
                if (powerValue > 0)
                    powerValue -= consumPowerValue;
                else
                    powerValue = 0;
            }
        }
        //MousePosition
        if (!isPowerAllOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseStartPos = Input.mousePosition;
                ui_timeSlowImage.DOFade(timeSlowImageAlpha, 0.5f);
            }
            if (Input.GetMouseButton(0))
            {
                mouseEndPos = Input.mousePosition;
                Time.timeScale = originalTimeScale * timeScaleValue;
                Time.fixedDeltaTime = originalFixDeltaTime * timeScaleValue;
                isTimeSlow = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                rig.velocity = Vector3.zero;//发射的时候把速度清零，会更有操作感(为了对抗下落的速度)
                Time.timeScale = originalTimeScale;
                Time.fixedDeltaTime = originalFixDeltaTime;
                isAddForce = true;
                isTimeSlow = false;
                ui_timeSlowImage.DOFade(0, 0.5f);
            }
        }
        //AddForce
        mouseDir = (mouseStartPos - mouseEndPos).normalized;
        if (isAddForce && powerValue > 0)
        {
            float mouseLength = (mouseStartPos - mouseEndPos).magnitude;
            float force = forceValue * mouseLength * Time.deltaTime;
            rig.AddForce(force * mouseDir, ForceMode.Impulse);
            isAddForce = false;
        }

        //isDead
        if (isDead)
        {
        }
        if (isDeadEffect)
        {
            rig.constraints &= ~RigidbodyConstraints.FreezePositionZ;//解锁Z轴
            rig.velocity = Vector3.zero;
            Vector3 dir = new Vector3(cameraTrans.position.x, cameraTrans.position.y + 1, cameraTrans.position.z) - transform.position;
            rig.AddForce(dir * deadAddForce, ForceMode.Impulse);
            isDeadEffect = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Block") && !isDead)
        {
            isDead = true;
            isDeadEffect = true;
        }
    }
}
