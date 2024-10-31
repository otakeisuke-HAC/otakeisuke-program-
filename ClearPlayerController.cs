using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearPlayerController : MonoBehaviour
{
    Animator m_Animator; //�A�j���[�^�[
    private float m_Walk; //�����X�s�[�h
    private bool m_WalkFlag = false; //������

    // Start is called before the first frame update
    void Start()
    {
        //Animator�̎Q�Ƃ��擾
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //����̈ʒu�ɓ��B����܂ňړ�������
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
    //�^�C�g���ɖ߂�Ƃ��ɌĂ΂��֐�
    //�܂�������Ԃɖ߂�
    public void TitleWalking()
    {
        m_WalkFlag = true;
        transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
