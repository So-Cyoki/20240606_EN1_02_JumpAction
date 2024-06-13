using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrawArrow : MonoBehaviour
{
    private Image image;
    public bool isDraw;
    public Vector3 mouseStartPos;
    public Vector3 mouseEndPos;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        //MousePostion
        if (Input.GetMouseButtonDown(0))
        {
            isDraw = true;
            image.enabled = true;
            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDraw = false;
            image.enabled = false;
            mouseStartPos = Vector3.zero;
            mouseEndPos = Vector3.zero;
        }
        //ArrowDraw
        if (isDraw)
        {
            Vector3 mouseDir = mouseStartPos - mouseEndPos;
            float size = mouseDir.magnitude;
            image.rectTransform.sizeDelta = new Vector2(size, size);
            transform.position = mouseStartPos;
            Vector3 dir = mouseDir.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
    }

}
