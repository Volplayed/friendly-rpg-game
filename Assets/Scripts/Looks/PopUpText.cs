using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    //text mesh
    public TMP_Text textmesh;

    //time to disappear and destroy popUp text
    private float disappearTimer = 1f;

    //set text mesh values
    public void Setup(int value, Color color)
    {
        //set text
        textmesh.SetText(value.ToString());

        //set color
        textmesh.color = color;
    }

    //for animating popUp text
    private void Update()
    {
        //move speed
        float moveYSpeed = 20;

        //move up
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        //decrease disappear timer
        disappearTimer -= Time.deltaTime;

        //if disappear timer is less than 0
        if (disappearTimer < 0)
        {
            //start disappearing
            float disappearSpeed = 3;

            //get color
            Color color = textmesh.color;
            //decrease alpha
            color.a -= disappearSpeed * Time.deltaTime;
            //set new color
            textmesh.color = color;

            //if alpha is less than 0
            if (color.a < 0)
            {
                //destroy popUp text
                Destroy(gameObject);
            }
        }
    }
}