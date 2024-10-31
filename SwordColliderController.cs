using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderController : MonoBehaviour
{
    private BoxCollider m_SwordCollider; //�U������
    private BoxCollider m_SwordSkillCollider; //�X�L���̍U������
    AudioSource m_AudioSource;
    public AudioClip a_Skill; //�X�L����SE
    public AudioClip a_AttackVoice; //�A�^�b�N�{�C�X(�����I)SE
    public AudioClip a_AttackVoice2; //�A�^�b�N�{�C�X(�������I)SE

    Animator m_Animator;
    [SerializeField]
    private GameObject p_Attack; //�U���̃p�[�e�B�N��
    [SerializeField]
    private GameObject p_Skill; //�X�L���̍U���̃p�[�e�B�N��

    private void Start()
    {
        //SwordObject��BoxCollider���擾
        m_SwordCollider = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        //SwordSkillObject��BoxCollider���擾
        m_SwordSkillCollider = GameObject.FindGameObjectWithTag("Skill").GetComponent<BoxCollider>();
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        
    }
    //�U������̏���
    public void SwordColliderOn()
    {
        m_SwordCollider.enabled = true;
        GameObject Particle = Instantiate(p_Attack,m_SwordCollider.gameObject.transform.position,Quaternion.identity);
        Destroy(Particle, 2);
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            m_AudioSource.PlayOneShot(a_AttackVoice);
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
        {
            m_AudioSource.PlayOneShot(a_AttackVoice2);
        }       
    }

    public void SwordColliderOff()
    {
        m_SwordCollider.enabled = false;
        
    }
    //�X�L���̍U������̏���
    public void SwordSkillColliderOn()
    {
        m_SwordSkillCollider.enabled = true;
        m_AudioSource.PlayOneShot(a_Skill);
        GameObject Particle = Instantiate(p_Skill, m_SwordCollider.gameObject.transform.position, Quaternion.identity);
        Destroy(Particle, 3);
    }

    public void SwordSkillColliderOff()
    {
        m_SwordSkillCollider.enabled = false;
    }
}
