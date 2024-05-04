using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numTextChange : MonoBehaviour
{
    Text t;
    int num=0;
    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Text>();
    }
    public void addNum(int ans)
    {
        num += ans;
        t.text = num.ToString();
    }
    public void numChange(int num0)
    {
        t = this.GetComponent<Text>();
        num = num0;
        t.text = num.ToString();
    }    
    public void numfree()
    {
        StartCoroutine(moveText());
    }
    IEnumerator moveText()
    {
        yield return new WaitUntil(() => gameObject.transform.position == GameObject.Find("num").transform.position);
        GameObject.Find("num").GetComponent<numTextChange>().addNum(num);
        Destroy(this.gameObject);
    }

}
