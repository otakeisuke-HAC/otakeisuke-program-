using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerController m_Player; //�v���C���[
    public GameObject m_BossObj;�@//�{�X
    public GameObject GameOver; //�Q�[���I�[�o�[�I�u�W�F�N�g
    
    private bool m_AudioPlaying = true;�@
    private bool m_BossAudioStart = false; //�{�X��BGM�t���O
    private float m_BGMLoopCount = 0;�@//BGM���J��Ԃ��炷���߂̃J�E���g
    private bool m_OverButtonFlag = false; //�Q�[���I�[�o�[�����Ƃ��Ƀ{�^������x�����Z�b�g���邽�߂̃t���O
    AudioSource m_AudioSource;
    public AudioClip a_BossBGM; //�{�XBGM
    // Start is called before the first frame update
    void Start()
    {
        //Player��PlayerController���擾
        m_Player = GameObject.Find("Player").GetComponent<PlayerController>();
        //AudioSource�̎Q�Ƃ��擾
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[��HP��0�ɂȂ������̏���
        if (m_Player.m_HP <= 0)
        {
            //GameOver�̃I�u�W�F�N�g���o��������
            GameOver.SetActive(true);
            //BGM���~�߂�
            m_AudioSource.Stop();
            if(m_OverButtonFlag == false)
            {
                //�Q�[���I�[�o�[�{�^���̍Ē���{�^��(GameOverRetryButton)���擾���ăZ���N�g����
                GameObject.Find("GameOverRetryButton").GetComponent<Button>().Select();
                m_OverButtonFlag = true;
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                //�Q�[���I�[�o�[�{�^���̍Ē���{�^��(GameOverRetryButton)���擾���ăZ���N�g����
                GameObject.Find("GameOverRetryButton").GetComponent<Button>().Select();
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //�|�[�Y�{�^���̖߂�{�^��(BackButton)���擾���ăZ���N�g����
            GameObject.Find("BackButton").GetComponent<Button>().Select();
        }
        BossBGM();

        BGMLoop();

    }
    //������B��������BGM��������x�����֐�
    void BGMLoop()
    {
        if(m_BGMLoopCount >= 0)
        {
            m_BGMLoopCount -= Time.deltaTime;
        }
    }
    //�{�X��ɓ��������̏���(BGM)
    void BossBGM()
    {
        //�{�X�����ꂽ�獡��BGM���~�߂�
        if (m_BossObj.activeSelf == true && m_AudioPlaying == true)
        {
            m_AudioSource.Stop();
            m_AudioPlaying = false;
        }
        //�{�X��BGM���~�߂���{�X��BGM��炷
        else if (m_AudioPlaying == false && m_BGMLoopCount <= 0)
        {
            m_AudioSource.PlayOneShot(a_BossBGM);
            m_BossAudioStart = true;
            if (m_BossAudioStart == true)
            {
                m_BGMLoopCount = 120;�@//BGM�̍Đ�����(�I�������܂��Đ�������)
                m_BossAudioStart = false;
            }

        }
    }
}
