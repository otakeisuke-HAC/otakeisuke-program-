using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    
    private GameObject PlayerObj; // プレイヤー
    public EnemySearchManager SearchManager;

    public float m_EnemyPatrolSpeed; //巡回時のスピード
    public float m_EnemyChaseSpeed; //追跡時のスピード
    Animator m_Animator;

    private float m_Speed; // エネミーのスピードを保存する変数
    private float m_MoveTimer; 
    private float m_ChangeTime = 5; //移動方向を変える時間
    public int hp = 5; //SlimeのHP

    bool m_Invincibility = false; //無敵フラグ
    float m_InvincibilityTime = 0; //無敵時間

    //エネミーのAttackColliderを取得
    public SphereCollider AttackCollider;

    private bool m_Found; //条件を満たしたら一回だけ発動するように制御するフラグs

    AudioSource m_AudioSource;
    public AudioClip a_Damage; //攻撃によるダメージSE
    public AudioClip a_Attack; //攻撃SE
    public AudioClip a_Surprise; //驚きSE

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
        //EnemySeachManagerからs_PlayerObjectを取得
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
            //指定した距離まで近づいたらEnemyのスピードを0にする
            if (distance <= 2)
            {
                m_EnemyChaseSpeed = 0;
                //Attackアニメーションの再生を止める
                if (PlayerObj.CompareTag("Player"))　//プレイヤーが死んでいたら攻撃の対象としないための条件文
                {
                    m_Animator.SetBool("Attack",true);
                }
                else
                {
                    m_Animator.SetBool("Attack",false);
                }               
            }
            else
            //範囲外ならスピードを戻す
            //Attackアニメーションの再生を止める
            {
                m_EnemyChaseSpeed = m_Speed;
                m_Animator.SetBool("Attack", false);
            }          
        }
        else
        {
            m_Found = false;
            m_MoveTimer += Time.deltaTime;

            // 自動前進
            transform.position += transform.forward * m_EnemyPatrolSpeed * Time.deltaTime;

            // 指定時間の経過（条件）
            if (m_MoveTimer > m_ChangeTime)
            {
                // 進路をランダムに変更する
                Vector3 course = new Vector3(0, Random.Range(0, 360), 0);
                transform.localRotation = Quaternion.Euler(course);

                // タイムカウントを０に戻す
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

    //エネミーが攻撃したら判定を出す関数
    void SlimeAttack()
    {
        AttackCollider.enabled = !AttackCollider.enabled;
        m_AudioSource.PlayOneShot(a_Attack);
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
