using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    //单例模式实现
    public static GM gm
    {
        get;
        private set;
    }
    private void Awake()
    {
        gm = this;
    }

    public int xNum = 10;
    public int yNum = 7;
    //对象生成的x，y坐标偏移
    public GameObject jectOffset;
    public float xOffset = -3f;
    public float yOffset = -3f;
    public int 方块数量 = 0;
    public GameObject[] 方块对象;

    /// <summary>
    /// 存放方块的二元动态数组
    /// </summary>
    public ArrayList square;
    Move move;
    // Start is called before the first frame update
    void Start()
    {
        move = GameObject.Find("GM").GetComponent<Move>();

        square   = new ArrayList();
        for(int xIndex =0;xIndex<xNum;xIndex++)
        {
            ArrayList temp = new ArrayList();
            for(int yIndex=0;yIndex<yNum;yIndex++)
            {
                GameObject g = NewObject();
                g.GetComponent<gamer>().positionChange(xIndex + jectOffset.transform.position.x, yIndex + jectOffset.transform.position.y);
                temp.Add(g);
            }
            square.Add(temp);            
        }

        StartCoroutine(isMove());
        StartCoroutine(startDel());
    }

    IEnumerator startDel()
    {
        yield return new WaitForSeconds(0.01f);
        清除 清除 = GameObject.Find("GM").GetComponent<清除>();
        清除.Del();
    }

    /// <summary>
    /// 当没有对象移动时，将标志置 1
    /// </summary>
    /// <returns></returns>
    IEnumerator isMove()
    {
        while(true)
        {
            yield return new WaitUntil(() => moveJectNum==0);
            if(!GameObject.Find("面板"))
            {
                moveBool = true;
            }
        }
    }

    /// <summary>
    /// 创建一个随机图案的对象
    /// </summary>
    public GameObject NewObject()
    {
        int conunt = 方块数量 + 1;
        if (conunt > 方块对象.Length||conunt <= 0)
        {
            conunt = 方块对象.Length;
        }
        GameObject g = Instantiate(方块对象[Random.Range(1, conunt)]);
        g.transform.parent = GameObject.Find("方块组").transform;
        return g;
    }

    /// <summary>
    /// 能否移动方块
    /// </summary>
    public bool moveBool = true;
    public int moveJectNum = 0;
    public void changeMoveBool()
    {
        moveJectNum--;
    }
    public bool getMoveBool()
    {
        return moveBool;
    }
    /// <summary>
    /// 判断两个对象是否相邻，是返回true，否返回false
    /// </summary>
    public bool jectAd(GameObject g1,GameObject g2)
    {
        gamer gamer1, gamer2;
        gamer1 = g1.GetComponent<gamer>();
        gamer2 = g2.GetComponent<gamer>();
        if(Mathf.Abs(gamer1.xIndex-gamer2.xIndex)+Mathf.Abs(gamer1.yIndex-gamer2.yIndex)==1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    /// <summary>
    /// 存储交换方块的临时链表
    /// </summary>
    List<GameObject> moveList;
    /// <summary>
    /// 添加交换方块的方法
    /// </summary>
    /// <param name="game"></param>
    public void moveListAdd(GameObject game)
    {
        if(moveBool)
        {
            if(moveList==null)
            {
                moveList = new List<GameObject>();
            }

            moveList.Add(game);
            if(moveList.Count==2)
            {
                if(jectAd(moveList[0],moveList[1]))
                {
                    //print("链表填入两个");
                    StartCoroutine(isMove(moveList[0], moveList[1]));
                    move.move(moveList[0], moveList[1]);
                }
                moveList[0].gameObject.GetComponent<gamer>().colocChange();
                moveList[1].gameObject.GetComponent<gamer>().colocChange();
                moveList.Clear();
            }
        }
        else
        {
            game.gameObject.GetComponent<gamer>().colocChange();
        }

    }
    /// <summary>
    /// 交换交换的两个方块的信息
    /// </summary>
    /// <param name="ob1"></param>
    /// <param name="ob2"></param>
    /// <param name="str"></param>
    void swap(object ob1, object ob2,string str)
    {
        if(ob1.GetType()!=ob2.GetType())
        {
            Debug.Log(ob1.ToString() + "与" + ob2.ToString() + "的值类型不同");
        }
        switch(str)
        {
            case "gamer":
                {
                    GameObject g1 = ob1 as GameObject;
                    GameObject g2 = ob2 as GameObject;

                    float f=0;
                    f = g1.GetComponent<gamer>().xIndex;
                    g1.GetComponent<gamer>().xIndex = g2.GetComponent<gamer>().xIndex;
                    g2.GetComponent<gamer>().xIndex = f;
                    f = g1.GetComponent<gamer>().yIndex;
                    g1.GetComponent<gamer>().yIndex = g2.GetComponent<gamer>().yIndex;
                    g2.GetComponent<gamer>().yIndex = f;

                    ArrayList temp;
                    temp = square[(int)g1.GetComponent<gamer>().xIndex] as ArrayList;
                    temp[(int)g1.GetComponent<gamer>().yIndex] = g1;
                    temp = square[(int)g2.GetComponent<gamer>().xIndex] as ArrayList;
                    temp[(int)g2.GetComponent<gamer>().yIndex] = g2;
                    break;
                }
        }
    }

    bool backJect = false;
    /// <summary>
    /// 判断两个方块交换是否完成
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <returns></returns>    
    public IEnumerator isMove(GameObject g1, GameObject g2)
    {
        moveJectNum++;
        moveBool = false;
        Vector3  t1, t2;
        t1 = g1.transform.position;
        t2 = g2.transform.position;
        swap(g1, g2, "gamer");
        yield return new WaitUntil(()=> g1.transform.position == t2 && g2.transform.position == t1);
        changeMoveBool();
        清除 清除 = GameObject.Find("GM").GetComponent<清除>();
        清除.Del();
        //if(清除.Del())
        //{
        //    if(backJect)
        //    {
        //        backJect = false;
        //        yield break;
        //    }
        //    moveListAdd(g1);
        //    moveListAdd(g2);
        //    backJect = true;
        //}
    }
    public IEnumerator isMove(GameObject g1, Vector3 pos)
    {
        moveJectNum++;
        moveBool = false;
        Vector3 t1;
        t1 = g1.transform.position;        
        yield return new WaitUntil(() => g1.transform.position == pos);
        changeMoveBool();
        if (moveJectNum == 0)
        {
            清除 清除 = GameObject.Find("GM").GetComponent<清除>();
            清除.Del();
        }
    }

}
