using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamer : MonoBehaviour
{
    //对象的x，y坐标
    public float xIndex = 0;
    public float yIndex = 0;

    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(GM.gm.getMoveBool())
        {
            //print("点击");
            colocChange();
            GM.gm.moveListAdd(gameObject);
        }
    }

    public void colocChange()
    {
        if (SpriteRenderer.color.a == 1f)
        {
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 0.3f);
        }
        else
        {
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1f);
        }
    }

    public void positionChange(float _xIndex,float _yIndex)
    {
        gameObject.transform.position = new Vector3(_xIndex, _yIndex , 0);
        xIndex = _xIndex- GM.gm.jectOffset.transform.position.x;
        yIndex = _yIndex- GM.gm.jectOffset.transform.position.y;

    }

    public void IndexChange(float x,float y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void textFree(GameObject g,int num)
    {
        GameObject text;
        text = Instantiate(GameObject.Find("num"));
        text.name = g.name;
        text.transform.SetParent(GameObject.Find("text").transform);
        text.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z);
        text.transform.localScale = new Vector3(1, 1, 1);
        //text.transform.position = new Vector3(0,0,0);
        Vector3 v3 = GameObject.Find("num").transform.position;
        //iTween.MoveTo(text, v3, 1f);
        iTween.MoveTo(text, iTween.Hash("easetype", "easeInBack", "time", 1, "position", v3));
        text.GetComponent<numTextChange>().numfree();
        text.GetComponent<numTextChange>().numChange(num);
    }
    public bool f = false;
    private void OnMouseEnter()
    {
        if(f)
        {
            colocChange();
        }
        
    }
    private void OnMouseExit()
    {
        if (f)
        {
            colocChange();
        }
    }
}
