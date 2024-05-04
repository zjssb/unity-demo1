using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{


    public float moveTime;
    public void move(GameObject g1,GameObject g2)
    {
        iTween.MoveTo(g1, g2.transform.position, 1f * moveTime);
        iTween.MoveTo(g2, g1.transform.position, 1f * moveTime);

    }

    //public float downTime;
    public void downMove(GameObject on,Vector3 down,float times)
    {
        iTween.MoveTo(on, down,times);
    }
}
