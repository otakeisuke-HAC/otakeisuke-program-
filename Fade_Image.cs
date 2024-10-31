using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Image : MonoBehaviour
{
    float alfa;
    float speed = 0.01f;
    float red, green, blue;
    private float count; //�t�F�[�h�܂ł̃J�E���g
    public float fadetime; //�t�F�[�h����܂ł̎���
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }
    void Update()
    {
        count += Time.deltaTime;
        //���ԂɂȂ�����t�F�[�h�C������
        if (count > fadetime)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
        }
    }
}
