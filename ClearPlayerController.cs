using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearPlayerController : MonoBehaviour
{
    Animator m_Animator; //アニメーター
    private float m_Walk; //歩くスピード
    private bool m_WalkFlag = false; //歩くか

    // Start is called before the first frame update
    void Start()
    {
        //Animatorの参照を取得
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //特定の位置に到達するまで移動させる
        transform.Translate(0,0,m_Walk);
        if(transform.position.x >= 0 && m_WalkFlag == false)
        {
            m_Walk = 0;
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            m_Walk = 0.05f;
        }

        m_Animator.SetFloat("Walk", m_Walk);
    }
    //タイトルに戻るときに呼ばれる関数
    //また歩く状態に戻す
    public void TitleWalking()
    {
        m_WalkFlag = true;
        transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
