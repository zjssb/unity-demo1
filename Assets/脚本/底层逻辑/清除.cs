using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 清除 : MonoBehaviour
{
    /// <summary>
    /// 方块的x,y分布二元嵌套数组
    /// ArrayList temp;
    ///temp = List[x] as ArrayList;
    /// </summary>
    ArrayList List = new ArrayList();


    // Start is called before the first frame update
    void Start()
    {

    }

    private void jectChangeNull(object ject)
    {
        List = GM.gm.square;
        GameObject g = ject as GameObject;
        ArrayList List2;
        List2 = List[(int)g.GetComponent<gamer>().xIndex] as ArrayList;
        List2[(int)g.GetComponent<gamer>().yIndex] = null;

    }
    /// <summary>
    /// 对所有方块遍历 查找是否存在可消除的方块
    /// 如果没有可消除的方块，则返回false ，否则返回true
    /// </summary>
    public bool Del()
    {
        List = GM.gm.square;
        bool flag = false;
        for (int i=0;i<List.Count;i++)
        {
            ArrayList temp;
            temp = List[i] as ArrayList;
            
            for(int j=0;j<temp.Count;j++)
            {
                if(Del1(i, j, temp)>0)
                {
                    flag = true;
                }
            }
        }
        Del2();
        return flag;
    }
   /// <summary>
   /// 判断方块是否可消除并将相关方块放入消除数组
   /// </summary>
   /// <param name="x_Row">方块的x</param>
   /// <param name="y_Col">方块的y</param>
   /// <param name="temp">ArraList[x_Row]</param>
    private int Del1(int x_Row,int y_Col,ArrayList temp)
    {
        int num = 0;
        ArrayList textList;
        int i = 0,ans=0;
        for(i=x_Row;i<List.Count;i++)//判断x轴方向是否存在可消除的方块
        {
            textList = List[i] as ArrayList;
            GameObject g1, g2;
            g1 = temp[y_Col]as GameObject;
            g2 = textList[y_Col] as GameObject;
            if (g1!=null&&g2!=null&&g1.name == g2.name)
            {
                ans++;
            }
            else
            {
                break;
            }

            if(i==x_Row+3&&ans<3)
            {       
                break;
            }
            
        }
        if(ans>=3)
        {
            num += ans;
            for (i = x_Row; i <x_Row+ ans; i++)
            {
                textList = List[i] as ArrayList;
                //if(DelListAdd(textList[y_Col]))
                //{
                //    textList[y_Col] = null;
                //}
                DelListAdd(textList[y_Col]);
            }
        }
        ans = 0;
        for(i=y_Col;i<temp.Count;i++)//判断y轴方向是否存在可消除的方块
        {
            GameObject g1, g2;
            g1 = temp[y_Col] as GameObject;
            g2 = temp[i] as GameObject;
            if (g1!=null&&g2!=null&&g1.name == g2.name)
            {
                ans++;
            }
            else
            {
                break;
            }
            if(i==y_Col+3&&ans<3)
            {
                break;
            }
        }
        if(ans>=3)
        {
            num += ans;
            for(i=y_Col;i<y_Col+ ans;i++)
            {
                //if(DelListAdd(temp[i]))
                //{
                //    temp[i] = null;
                //}
                DelListAdd(temp[i]);

            }
        }
        return num;
    }
    /// <summary>
    /// 消除存入的数组
    /// </summary>
    ArrayList DelList = new ArrayList();
    public bool DelListAdd(object ject)
    {        
        if (ject!=null)
        {
            GameObject g = ject as GameObject;
            if(!DelList.Contains(ject))
            {
                DelList.Add(ject);
            }
            else
            {
                g.GetComponent<gamer>().textFree(g, 200);
                return true;
            }
            g.GetComponent<gamer>().textFree(g, 100);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 消除方块方法
    /// </summary>
    private void Del2()
    {
        List = GM.gm.square;
        ArrayList temp;
        foreach(GameObject g in DelList )
        {
            jectChangeNull(g);
            GameObject Newg = GM.gm.NewObject();
            temp = List[(int)g.GetComponent<gamer>().xIndex] as ArrayList;
            temp.Add(Newg);
            Newg.GetComponent<gamer>().positionChange(
                g.GetComponent<gamer>().xIndex 
                + GM.gm.jectOffset.transform.position.x, temp.IndexOf(Newg) 
                + GM.gm.jectOffset.transform.position.y
                );
            //g.GetComponent<gamer>().textFree(g);
            Destroy(g);
        }
        DelList.Clear();
        GameObject.Find("GM").GetComponent<down>().startDown();

    }
}
