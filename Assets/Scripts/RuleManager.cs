using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    Transform startPoint;
    public GameObject block;
    Transform emptyBlock;
    List<EmptyCheck> emptyChecks = new();
    public Vector3 targetLeftUpPos;//目标的左上角开始坐标
    public int targetLine;//目标的行数
    public int targetRow;//目标的列数
    public float cubeSize;//方块的大小
    [Tooltip("一次至多生成多少个方块")]
    public int newNum;
    int prevTargetLine;//用于记录前一次生成的位置行数，至少一次不重复
    public UI_ScoreText ui_ScoreText;//用于加分处理，所以把脚本保存在这里
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
        //随机生成出一行方块，作为障碍物
        Vector3 startPos = new();//方块生成的位置
        int randLine = Random.Range(0, targetLine);//随机一行
        while (randLine == prevTargetLine)//如果随机出来的行和前一次一样，就再生成一次
            randLine = Random.Range(0, targetLine);
        int randRow = Random.Range(0, targetRow);//随机从一行的第几格开始出现
        startPos.y = targetLeftUpPos.y - randLine * cubeSize;
        startPos.x = targetLeftUpPos.x + randRow * cubeSize;
        startPos.z = startPoint.position.z;
        //一行生成多少次方块
        int frequency = Random.Range(1, targetRow - randRow);
        if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < frequency; i++)
            {
                Instantiate(block, new(startPos.x + i * 1, startPos.y, startPos.z), Quaternion.identity, this.transform);
            }
        prevTargetLine = randLine;
    }

    //随机位置：这个版本是返回没有方块占位置的目标pos
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
