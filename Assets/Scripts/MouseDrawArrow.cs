using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrawArrow : MonoBehaviour
{
    Image image;
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
            float sizeY = mouseDir.magnitude;
            float sizeX = sizeY / 5;
            if (sizeX < 100)
                sizeX = 100;
            image.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            transform.position = mouseStartPos;
            Vector3 dir = mouseDir.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
    }

}
