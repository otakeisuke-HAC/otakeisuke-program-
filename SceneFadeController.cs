
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeController : MonoBehaviour
{
    //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    private bool isFadeOut = false;
    //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O
    private bool isFadeIn = true;
    //�����x���ς��X�s�[�h
    float fadeSpeed = 0.75f;
    //��ʂ��t�F�[�h�����邽�߂̉摜���p�u���b�N�Ŏ擾
    [HideInInspector]
    public Image fadeImage;
    float red, green, blue, alfa;
    //�V�[���J�ڂ̂��߂̌^
    string afterScene;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        SetRGBA(0, 0, 0, 1);
        //�V�[���J�ڂ����������ۂɃt�F�[�h�C�����J�n����悤�ɐݒ�
        SceneManager.sceneLoaded += fadeInStart;
    }
    //�V�[���J�ڂ����������ۂɃt�F�[�h�C�����J�n����悤�ɐݒ�
    void fadeInStart(Scene scene, LoadSceneMode mode)
    {
        isFadeIn = true;
    }
    /// <summary>
    /// �t�F�[�h�A�E�g�J�n���̉摜��RGBA�l�Ǝ��̃V�[�������w��
    /// </summary>
    /// <param name="red">�摜�̐Ԑ���</param>
    /// <param name="green">�摜�̗ΐ���</param>
    /// <param name="blue">�摜�̐���</param>
    /// <param name="alfa">�摜�̓����x</param>
    /// <param name="nextScene">�J�ڐ�̃V�[����</param>
    
    //�t�F�[�h�A�E�g����
    public void fadeOutStart(int red, int green, int blue, int alfa, string nextScene)
    {
        SetRGBA(red, green, blue, alfa);
        SetColor();
        isFadeOut = true;
        afterScene = nextScene;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            if (isFadeIn == true)
            {
                //�s�����x�����X�ɉ�����
                alfa -= fadeSpeed * Time.deltaTime;
                //�ύX���������x���摜�ɔ��f������֐����Ă�
                SetColor();
                if (alfa <= 0)
                    isFadeIn = false;
            }
            if (isFadeOut == true)
            {
                //�s�����x�����X�ɏグ��
                alfa += fadeSpeed * Time.deltaTime;
                //�ύX���������x���摜�ɔ��f������֐����Ă�
                SetColor();
                if (alfa >= 1)
                {
                    isFadeOut = false;
                    SceneManager.LoadScene(afterScene);
                    afterScene = null;
                }
            }

            //fadeImage�I�u�W�F�N�g���擾
            //LoadScene�œ����V�[����ǂݍ���ł��I�u�W�F�N�g���擾�ł���悤�ɂ��邽��Update�ŃI�u�W�F�N�g�̃^�O�Ŏ擾
            //�����Ə��������Ȃ����߂Ɉ�x���߂��I�u�W�F�N�g���擾������������������Ȃ��悤�ɂ���
            if (fadeImage != GameObject.FindGameObjectWithTag("SceneFadeImage").GetComponent<Image>())
            {
                fadeImage = GameObject.FindGameObjectWithTag("SceneFadeImage").GetComponent<Image>();
                Debug.Log(fadeImage.ToString());
            }
        }
        
    }
    //�摜�ɐF��������֐�
    void SetColor()
    {
        if(fadeImage != null)
        {
            fadeImage.color = new Color(red, green, blue, alfa);
        }
    }
    //�F�̒l��ݒ肷�邽�߂̊֐�
    public void SetRGBA(int r, int g, int b, int a)
    {
        red = r;
        green = g;
        blue = b;
        alfa = a;
    }
}
