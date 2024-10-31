using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Image : MonoBehaviour
{
    float alfa;
    float speed = 0.01f;
    float red, green, blue;
    private float count; //フェードまでのカウント
    public float fadetime; //フェードするまでの時間
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }
    void Update()
    {
        count += Time.deltaTime;
        //時間になったらフェードインする
        if (count > fadetime)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
        }
    }
}
