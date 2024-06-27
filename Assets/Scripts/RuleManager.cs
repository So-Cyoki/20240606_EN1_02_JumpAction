using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    Transform startPoint;
    public GameObject block;
    Transform emptyBlock;
    List<EmptyCheck> emptyChecks = new();
    private void Awake()
    {
        startPoint = transform.Find("StartPoint");
        emptyBlock = transform.Find("EmptyBlock");
    }

    private void Start()
    {
        foreach (Transform item in emptyBlock)
        {
            emptyChecks.Add(item.GetComponent<EmptyCheck>());
        }
    }

    private void Update()
    {
        Vector3 pos = RandomSelect();
        startPoint.position = new(pos.x, pos.y, startPoint.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(block, startPoint.position, Quaternion.identity, this.transform);
        }
    }

    Vector3 RandomSelect()
    {
        Vector3 pos = new();
        List<Vector3> emptys = new();
        foreach (var item in emptyChecks)
        {
            if (!item.isBlockOn)
                emptys.Add(item.transform.position);
        }
        if (emptys.Count <= 0)
            return pos;
        pos = emptys[Random.Range(0, emptys.Count)];
        return pos;
    }
}
