using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController m_Player; //プレイヤー
    public GameObject m_BossObj;　//ボス
    public GameObject GameOver; //ゲームオーバーオブジェクト
    
    private bool m_AudioPlaying = true;　
    private bool m_BossAudioStart = false; //ボスのBGMフラグ
    private float m_BGMLoopCount = 0;　//BGMを繰り返し鳴らすためのカウント
    private bool m_OverButtonFlag = false; //ゲームオーバーしたときにボタンを一度だけセットするためのフラグ
    AudioSource m_AudioSource;
    public AudioClip a_BossBGM; //ボスBGM
    // Start is called before the first frame update
    void Start()
    {
        //PlayerのPlayerControllerを取得
        m_Player = GameObject.Find("Player").GetComponent<PlayerController>();
        //AudioSourceの参照を取得
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーのHPが0になった時の処理
        if (m_Player.m_HP <= 0)
        {
            //GameOverのオブジェクトを出現させる
            GameOver.SetActive(true);
            //BGMを止める
            m_AudioSource.Stop();
            if(m_OverButtonFlag == false)
            {
                //ゲームオーバーボタンの再挑戦ボタン(GameOverRetryButton)を取得してセレクトする
                GameObject.Find("GameOverRetryButton").GetComponent<Button>().Select();
                m_OverButtonFlag = true;
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                //ゲームオーバーボタンの再挑戦ボタン(GameOverRetryButton)を取得してセレクトする
                GameObject.Find("GameOverRetryButton").GetComponent<Button>().Select();
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //ポーズボタンの戻るボタン(BackButton)を取得してセレクトする
            GameObject.Find("BackButton").GetComponent<Button>().Select();
        }
        BossBGM();

        BGMLoop();

    }
    //条件を達成したらBGMをもう一度流す関数
    void BGMLoop()
    {
        if(m_BGMLoopCount >= 0)
        {
            m_BGMLoopCount -= Time.deltaTime;
        }
    }
    //ボス戦に入った時の処理(BGM)
    void BossBGM()
    {
        //ボスが現れたら今のBGMを止める
        if (m_BossObj.activeSelf == true && m_AudioPlaying == true)
        {
            m_AudioSource.Stop();
            m_AudioPlaying = false;
        }
        //ボスのBGMを止めたらボスのBGMを鳴らす
        else if (m_AudioPlaying == false && m_BGMLoopCount <= 0)
        {
            m_AudioSource.PlayOneShot(a_BossBGM);
            m_BossAudioStart = true;
            if (m_BossAudioStart == true)
            {
                m_BGMLoopCount = 120;　//BGMの再生時間(終わったらまた再生させる)
                m_BossAudioStart = false;
            }

        }
    }
}
