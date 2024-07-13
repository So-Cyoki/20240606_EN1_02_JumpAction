using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    Transform startPoint;
    public GameObject block;
    public GameObject item_power;
    Transform emptyBlock;
    List<EmptyCheck> emptyChecks = new();
    public Vector3 targetLeftUpPos;//目标的左上角开始坐标
    public int targetLine;//目标的行数
    public int targetRow;//目标的列数
    public float cubeSize;//方块的大小
    int prevTargetLine;//用于记录前一次生成的位置行数，至少一次不重复
    int itemBornChances;//生成Item的几率
    public int itemBornChancesAdd;//不生成的话，每回加多少概率
    public MouseMove mouseMove;
    public UI_ScoreText ui_ScoreText;//用于加分处理，所以把脚本保存在这里
    public int oneShotNumber;//一次生成多少个
    public float oneShotTime;//每次生成之间的间隔
    float currentTime_oneShotTime;
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
        if (!mouseMove.isDead)
            GameRule();
    }

    void GameRule()
    {
        int score = ui_ScoreText.score;
        int oneShotNumberOffset = 0;
        if (score <= 50)
            oneShotNumberOffset = oneShotNumber * 1;
        else if (score <= 150)
            oneShotNumberOffset = oneShotNumber * 2;
        else if (score <= 250)
            oneShotNumberOffset = oneShotNumber * 3;
        else if (score <= 450)
            oneShotNumberOffset = oneShotNumber * 4;
        else if (score <= 650)
            oneShotNumberOffset = oneShotNumber * 5;
        //关卡生成
        bool isStart = false;
        currentTime_oneShotTime -= Time.deltaTime;
        if (currentTime_oneShotTime < 0)
        {
            isStart = true;
            currentTime_oneShotTime = oneShotTime;
        }
        if (isStart)
        {
            for (int i = 0; i < oneShotNumberOffset; i++)
                CubeRandomInstantiate();
        }
    }

    void CubeRandomInstantiate()
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
        //不生成方块，生成Item的随机计算
        bool isItemBorn = false;
        int itemBorn = Random.Range(0, 100);
        if (itemBorn <= itemBornChances)
        {
            isItemBorn = true;
            itemBornChances = 0;
        }
        else
            itemBornChances += itemBornChancesAdd;

        //生成方块还是Item
        if (!isItemBorn)
        {
            //一行生成多少次方块
            int frequency = Random.Range(1, targetRow - randRow);
            //if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < frequency; i++)
            {
                Instantiate(block, new(startPos.x + i * 1, startPos.y, startPos.z), Quaternion.identity, this.transform);
            }
        }
        else
        {
            Instantiate(item_power, new(startPos.x, startPos.y, startPos.z), Quaternion.identity, this.transform);
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

    public void Restart()
    {
        List<Transform> deleteObjList = new();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Block") || child.CompareTag("Item"))
                deleteObjList.Add(child);
        }
        foreach (Transform item in deleteObjList)
            Destroy(item.gameObject);
    }
}