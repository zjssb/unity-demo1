using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 道具拖动 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Vector3 off;
    private Vector3 mousePos;
    private void OnMouseDown()
    {
        mousePos = Input.mousePosition;
        //将世界坐标转化为屏幕坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //获取当前物体与鼠标的偏差
        off = transform.position - Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void OnMouseDrag()
    {
        //将屏幕坐标转化为世界坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //加上偏差使物体与鼠标相对位置不变
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + off;
    }

}
