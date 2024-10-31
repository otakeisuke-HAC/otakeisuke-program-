using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyGolemController : MonoBehaviour
{

    private GameObject m_PlayerObj; // �v���C���[
    public EnemySearchManager m_SearchManager;

    public float m_EnemySpeed; //�ړ����x
    Animator m_Animator; 
    private float m_Attack2Count = 10; //StrongAttack������܂ł̃J�E���g
    
    private float m_Speed; //Golem�̃X�s�[�h��ۑ�����ϐ�
    public int hp; //Golem��HP
    bool m_Invincibility = false; //���G�t���O
    float m_InvincibilityTime = 0; //���G����
    
    private SphereCollider m_AttackCollider;�@//Golem��AttackCollider���擾
    private SphereCollider m_StrongAttackCollider;�@//Golem��StrongAttackCollider���擾

    private bool m_Found; ////�����t���O(�����𖞂��������񂾂���������悤�ɐ��䂷��)

    AudioSource m_AudioSource;
    public AudioClip a_Damage; //�U���ɂ��_���[�WSE
    public AudioClip a_Attack; //�U��SE
    public AudioClip a_StrongAttack; //�����U��SE
    public AudioClip a_Surprise; //����SE
    public AudioClip a_Die; //���SSE

    [SerializeField]
    private GameObject p_Attack; //�U���̃p�[�e�B�N��
    [SerializeField]
    private GameObject p_StrongAttack; //�����U���̃p�[�e�B�N��


    // Start is called before the first frame update
    void Start()
    {
        //Animator�̎Q�Ƃ��擾
        m_Animator = GetComponent<Animator>();
        //Golem�̎q�I�u�W�F�N�g��AttackCollider���擾(���Ԓʂ�ɒu��)
        m_AttackCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        m_StrongAttackCollider = transform.GetChild(1).GetComponent<SphereCollider>();
        //Golem�̃X�s�[�h��ۑ�
        m_Speed = m_EnemySpeed;
        m_Found = false; 
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = Vector3.zero;
        speed.z = m_EnemySpeed;
        
        // PlayerObject���擾
        m_PlayerObj = m_SearchManager.GetComponent<EnemySearchManager>().s_PlayerObj;

        if (m_PlayerObj != null && hp > 0)
        {

            if (m_Found == false)
            {
                Found();
                m_Found = true;
            }
            transform.LookAt(m_PlayerObj.transform.position);

            float distance = Vector3.Distance(m_PlayerObj.transform.position, transform.position);
            //�w�肵�������܂ŋ߂Â�����Enemy�̃X�s�[�h��0�ɂ���
            if (distance <= 5)
            {
                m_EnemySpeed = 0;
                //Attack�A�j���[�V�����̍Đ����~�߂�
                if (m_PlayerObj.CompareTag("Player"))�@//�v���C���[������ł�����U���̑ΏۂƂ��Ȃ����߂̏�����
                {
                    m_Animator.SetBool("Attack", true);
                    
                }
                else
                {
                    m_Animator.SetBool("Attack", false);
                }
            }
            else
            //�͈͊O�Ȃ�X�s�[�h��߂�
            //Attack�A�j���[�V�����̍Đ����~�߂�
            {
                m_EnemySpeed = m_Speed;
                m_Animator.SetBool("Attack", false);
                GolemStrongAttackTimer();
            }
            this.transform.Translate(speed);
        }
        else
        {
            m_Found = false;
            
        }
        EnemyinvincibilityTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        //�U����H��������̏���
        if (other.gameObject.CompareTag("Sword") && m_Invincibility == false)
        {
            hp--; //HP�����炷����
            m_Invincibility = true; //���G�t���O
            m_AudioSource.PlayOneShot(a_Damage);
            if (hp <= 0)
            {
                m_Animator.SetTrigger("Die");
                m_AudioSource.PlayOneShot(a_Die);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            m_InvincibilityTime = 0.1f; //���G����
        }
        else if(other.gameObject.CompareTag("Skill") && m_Invincibility == false)
        {
            m_Animator.SetTrigger("Dizzy");
        }
    }
    //���G���Ԃ̏���
    void EnemyinvincibilityTime()
    {
        if (m_InvincibilityTime >= 0)
        {
            m_InvincibilityTime -= Time.deltaTime;

        }
        else if (m_InvincibilityTime <= 0)
        {
            m_Invincibility = false;
        }
    }

    //�G�U�������画����o���֐�
    void GolemAttackOn()
    {
        m_AttackCollider.enabled = true;
        m_AudioSource.PlayOneShot(a_Attack);
        GameObject Particl = Instantiate(p_Attack, m_AttackCollider.gameObject.transform.position,Quaternion.identity);
        Destroy(Particl,3f);
    }
    void GolemAttackOff()
    {
        m_AttackCollider.enabled = false;
    }
    //StrongAttack��ł܂ł̃J�E���g
    void GolemStrongAttackTimer()
    {
        
        if(m_Attack2Count < 0)
        {
            m_Animator.SetTrigger("StrongAttack");
            m_Attack2Count = 10;
        }
        else
        {
            m_Attack2Count -= Time.deltaTime;
        }
    }

    //�U�������画����o���֐�
    void GolemStrongAttackOn()
    {
        m_StrongAttackCollider.enabled = true;
        m_AudioSource.PlayOneShot(a_StrongAttack);
        GameObject Particl = Instantiate(p_StrongAttack, m_AttackCollider.gameObject.transform.position, Quaternion.identity);
        Destroy(Particl, 3f);
    }
    void GolemStrongAttackOff()
    {
        m_StrongAttackCollider.enabled = false;
    }
    //�������A�N�V����
    void Found()
    {
        m_Animator.SetTrigger("Found");
        m_AudioSource.PlayOneShot(a_Surprise);
    }
    //���S
    void Die()
    {
        Destroy(gameObject);
    }
}
