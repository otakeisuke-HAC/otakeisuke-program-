using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderController : MonoBehaviour
{
    private BoxCollider m_SwordCollider; //攻撃判定
    private BoxCollider m_SwordSkillCollider; //スキルの攻撃判定
    AudioSource m_AudioSource;
    public AudioClip a_Skill; //スキルのSE
    public AudioClip a_AttackVoice; //アタックボイス(おりゃ！)SE
    public AudioClip a_AttackVoice2; //アタックボイス(ここだ！)SE

    Animator m_Animator;
    [SerializeField]
    private GameObject p_Attack; //攻撃のパーティクル
    [SerializeField]
    private GameObject p_Skill; //スキルの攻撃のパーティクル

    private void Start()
    {
        //SwordObjectのBoxColliderを取得
        m_SwordCollider = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        //SwordSkillObjectのBoxColliderを取得
        m_SwordSkillCollider = GameObject.FindGameObjectWithTag("Skill").GetComponent<BoxCollider>();
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        
    }
    //攻撃判定の処理
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
    //スキルの攻撃判定の処理
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
