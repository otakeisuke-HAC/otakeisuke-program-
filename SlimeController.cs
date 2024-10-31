using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    
    private GameObject PlayerObj; // �v���C���[
    public EnemySearchManager SearchManager;

    public float m_EnemyPatrolSpeed; //���񎞂̃X�s�[�h
    public float m_EnemyChaseSpeed; //�ǐՎ��̃X�s�[�h
    Animator m_Animator;

    private float m_Speed; // �G�l�~�[�̃X�s�[�h��ۑ�����ϐ�
    private float m_MoveTimer; 
    private float m_ChangeTime = 5; //�ړ�������ς��鎞��
    public int hp = 5; //Slime��HP

    bool m_Invincibility = false; //���G�t���O
    float m_InvincibilityTime = 0; //���G����

    //�G�l�~�[��AttackCollider���擾
    public SphereCollider AttackCollider;

    private bool m_Found; //�����𖞂��������񂾂���������悤�ɐ��䂷��t���Os

    AudioSource m_AudioSource;
    public AudioClip a_Damage; //�U���ɂ��_���[�WSE
    public AudioClip a_Attack; //�U��SE
    public AudioClip a_Surprise; //����SE

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        AttackCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        m_Speed = m_EnemyChaseSpeed;
        m_Found = false;
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //EnemySeachManager����s_PlayerObject���擾
        PlayerObj = SearchManager.GetComponent<EnemySearchManager>().s_PlayerObj;
        
        if (PlayerObj != null && hp > 0)
        {
            
            transform.position += transform.forward * m_EnemyChaseSpeed * Time.deltaTime;
            if (m_Found == false)
            {
                Found();
                m_Found = true;
            }
            transform.LookAt(PlayerObj.transform.position);            

            float distance = Vector3.Distance(PlayerObj.transform.position, transform.position);
            //�w�肵�������܂ŋ߂Â�����Enemy�̃X�s�[�h��0�ɂ���
            if (distance <= 2)
            {
                m_EnemyChaseSpeed = 0;
                //Attack�A�j���[�V�����̍Đ����~�߂�
                if (PlayerObj.CompareTag("Player"))�@//�v���C���[������ł�����U���̑ΏۂƂ��Ȃ����߂̏�����
                {
                    m_Animator.SetBool("Attack",true);
                }
                else
                {
                    m_Animator.SetBool("Attack",false);
                }               
            }
            else
            //�͈͊O�Ȃ�X�s�[�h��߂�
            //Attack�A�j���[�V�����̍Đ����~�߂�
            {
                m_EnemyChaseSpeed = m_Speed;
                m_Animator.SetBool("Attack", false);
            }          
        }
        else
        {
            m_Found = false;
            m_MoveTimer += Time.deltaTime;

            // �����O�i
            transform.position += transform.forward * m_EnemyPatrolSpeed * Time.deltaTime;

            // �w�莞�Ԃ̌o�߁i�����j
            if (m_MoveTimer > m_ChangeTime)
            {
                // �i�H�������_���ɕύX����
                Vector3 course = new Vector3(0, Random.Range(0, 360), 0);
                transform.localRotation = Quaternion.Euler(course);

                // �^�C���J�E���g���O�ɖ߂�
                m_MoveTimer= 0;
            }
        }
        EnemyinvincibilityTime();
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Sword") && m_Invincibility == false)
        {
            hp--;
            m_Invincibility = true;
            m_AudioSource.PlayOneShot(a_Damage);
            if (hp > 0)
            {
                m_Animator.SetTrigger("Damage");
                
            }            
            else if(hp <= 0)
            {
                m_Animator.SetTrigger("Die");
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            m_InvincibilityTime = 0.1f;
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

    //�G�l�~�[���U�������画����o���֐�
    void SlimeAttack()
    {
        AttackCollider.enabled = !AttackCollider.enabled;
        m_AudioSource.PlayOneShot(a_Attack);
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
