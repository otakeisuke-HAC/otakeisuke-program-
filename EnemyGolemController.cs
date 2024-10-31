using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyGolemController : MonoBehaviour
{

    private GameObject m_PlayerObj; // プレイヤー
    public EnemySearchManager m_SearchManager;

    public float m_EnemySpeed; //移動速度
    Animator m_Animator; 
    private float m_Attack2Count = 10; //StrongAttackをするまでのカウント
    
    private float m_Speed; //Golemのスピードを保存する変数
    public int hp; //GolemのHP
    bool m_Invincibility = false; //無敵フラグ
    float m_InvincibilityTime = 0; //無敵時間
    
    private SphereCollider m_AttackCollider;　//GolemのAttackColliderを取得
    private SphereCollider m_StrongAttackCollider;　//GolemのStrongAttackColliderを取得

    private bool m_Found; ////発見フラグ(条件を満たしたら一回だけ発動するように制御する)

    AudioSource m_AudioSource;
    public AudioClip a_Damage; //攻撃によるダメージSE
    public AudioClip a_Attack; //攻撃SE
    public AudioClip a_StrongAttack; //強い攻撃SE
    public AudioClip a_Surprise; //発見SE
    public AudioClip a_Die; //死亡SE

    [SerializeField]
    private GameObject p_Attack; //攻撃のパーティクル
    [SerializeField]
    private GameObject p_StrongAttack; //強い攻撃のパーティクル


    // Start is called before the first frame update
    void Start()
    {
        //Animatorの参照を取得
        m_Animator = GetComponent<Animator>();
        //Golemの子オブジェクトのAttackColliderを取得(順番通りに置く)
        m_AttackCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        m_StrongAttackCollider = transform.GetChild(1).GetComponent<SphereCollider>();
        //Golemのスピードを保存
        m_Speed = m_EnemySpeed;
        m_Found = false; 
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = Vector3.zero;
        speed.z = m_EnemySpeed;
        
        // PlayerObjectを取得
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
            //指定した距離まで近づいたらEnemyのスピードを0にする
            if (distance <= 5)
            {
                m_EnemySpeed = 0;
                //Attackアニメーションの再生を止める
                if (m_PlayerObj.CompareTag("Player"))　//プレイヤーが死んでいたら攻撃の対象としないための条件文
                {
                    m_Animator.SetBool("Attack", true);
                    
                }
                else
                {
                    m_Animator.SetBool("Attack", false);
                }
            }
            else
            //範囲外ならスピードを戻す
            //Attackアニメーションの再生を止める
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
        //攻撃を食らった時の処理
        if (other.gameObject.CompareTag("Sword") && m_Invincibility == false)
        {
            hp--; //HPを減らす処理
            m_Invincibility = true; //無敵フラグ
            m_AudioSource.PlayOneShot(a_Damage);
            if (hp <= 0)
            {
                m_Animator.SetTrigger("Die");
                m_AudioSource.PlayOneShot(a_Die);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            m_InvincibilityTime = 0.1f; //無敵時間
        }
        else if(other.gameObject.CompareTag("Skill") && m_Invincibility == false)
        {
            m_Animator.SetTrigger("Dizzy");
        }
    }
    //無敵時間の処理
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

    //エ攻撃したら判定を出す関数
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
    //StrongAttackを打つまでのカウント
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

    //攻撃したら判定を出す関数
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
    //発見リアクション
    void Found()
    {
        m_Animator.SetTrigger("Found");
        m_AudioSource.PlayOneShot(a_Surprise);
    }
    //死亡
    void Die()
    {
        Destroy(gameObject);
    }
}
