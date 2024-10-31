using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private SceneFadeController m_SceneFadeController; //�t�F�[�h�p
    private Button m_PoseButton; //�|�[�Y�p

    // Start is called before the first frame update
    void Start()
    {
        //SceneFadeController���擾
        m_SceneFadeController = GameObject.Find("GameController").GetComponent<SceneFadeController>();
        //PoseButton��Button�̎Q�Ƃ��擾
        m_PoseButton = GameObject.Find("PoseButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^�����������Ƃ���PoseButton��onClick�֐��𔭓�������
        if(Input.GetButtonDown("Pose"))
        {
            m_PoseButton.onClick.Invoke();
        }
    }

    //�|�[�Y
    public void Pose()
    {
        Time.timeScale = 0;
    }
    //�^�C�g���ɖ߂�
    public void Title()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, "TitleScene");
    }
    //�ŏ������蒼��
    public void Retry()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, SceneManager.GetActiveScene().name);

    }
    //�߂�
    public void Back()
    {
        Time.timeScale = 1.0f;
    }
}
