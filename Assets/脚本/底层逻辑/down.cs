using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down : MonoBehaviour
{

    ArrayList list =new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        list = GM.gm.square;
    }
    //public void debug()
    //{
    //    jectDown();
    //}

    public float DownTime = 0.1f;
    /// <summary>
    /// 将被消除对象位置的上一个对象移动到这个位置
    /// </summary>
    private void jectDown(int x,int y)
    {
        Move down = GameObject.Find("GM").GetComponent<Move>();
        ArrayList temp;
        temp = list[x] as ArrayList;
        Vector3 pos;
        pos = new Vector3(x+ GM.gm.jectOffset.transform.position.x, y+ GM.gm.jectOffset.transform.position.y, 0);
        for(int i=y;i<temp.Count;i++)
        {
            if (temp[i] != null)
            {
                GameObject g = temp[i] as GameObject;
                down.downMove(g, pos, (i - y) * 0.1f);
                StartCoroutine(GM.gm.isMove(g,pos));
                temp[y] = temp[i];
                temp[i] = null;
                g.GetComponent<gamer>().IndexChange(x, y);
                return;//找到 消除对象上方的对象
            }            
        }
        
    }
    /// <summary>
    /// 判断被消除的对象的坐标位置
    /// </summary>
    public void startDown()
    {
        int i = 0,j=0;
        foreach(object ob1 in list)
        {
            ArrayList temp = ob1 as ArrayList;
            //foreach(object ob2 in (ob1 as ArrayList))
            for (int k=0;k< GM.gm.yNum ;k++)
            {
                object ob2 = temp[k];
                if (ob2 == null)
                {
                    jectDown(i, j);
                }
                j++;
            }
            i++;
            j = 0;
        }
        int flag = 0;
        foreach(object ob1 in list )
        {
            flag++;
            ArrayList temp = ob1 as ArrayList;
            if(temp.Count> GM.gm.yNum)
            {
                while(temp.Count> GM.gm.yNum)
                {
                    temp.Remove(null);
                }
            }
        }
    }


}
