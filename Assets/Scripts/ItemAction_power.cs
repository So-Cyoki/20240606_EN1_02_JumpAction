using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction_power : MonoBehaviour
{
    public GameObject cubeModel;
    public float speed;
    public bool isGet;
    public Color startColor;
    public Color endColor;
    Renderer cubeRenderer;
    public float color_targetPosZ;//到达什么位置之后变为最终颜色
    float color_moveLength;//一开始的距离是多少，用于判断移动到什么位置变化成什么颜色

    private void Start()
    {
        cubeRenderer = cubeModel.GetComponent<Renderer>();
        cubeRenderer.material.color = startColor;
        color_moveLength = Mathf.Abs(transform.position.z - color_targetPosZ);
    }

    private void Update()
    {
        if (!isGet)
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        Color colorLerp = Color.Lerp(startColor, endColor,
        Mathf.Abs((color_moveLength - transform.position.z) / color_moveLength));
        cubeRenderer.material.color = colorLerp;
        if (transform.position.z < -20)
            Destroy(gameObject);
    }
}
