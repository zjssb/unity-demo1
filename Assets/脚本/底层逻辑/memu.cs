using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class memu : MonoBehaviour
{
    public GameObject Plane;
    GM gm;
    private void Start()
    {
        Plane = GameObject.Find("���");
        Plane.SetActive(false);
        gm = GameObject.Find("GM").GetComponent<GM>();
    }

    public void memu_button()
    {

        Plane.SetActive(true);
        gm.moveBool = !gm.moveBool;
    }

    public void ����()
    {

        Plane.SetActive(false);
    }

    public void exit_button()
    {
        Application.Quit();
    }
}
