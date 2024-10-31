using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private SceneFadeController m_SceneFadeController; //フェード用
    private Button m_PoseButton; //ポーズ用

    // Start is called before the first frame update
    void Start()
    {
        //SceneFadeControllerを取得
        m_SceneFadeController = GameObject.Find("GameController").GetComponent<SceneFadeController>();
        //PoseButtonのButtonの参照を取得
        m_PoseButton = GameObject.Find("PoseButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズボタンを押したときにPoseButtonのonClick関数を発動させる
        if(Input.GetButtonDown("Pose"))
        {
            m_PoseButton.onClick.Invoke();
        }
    }

    //ポーズ
    public void Pose()
    {
        Time.timeScale = 0;
    }
    //タイトルに戻る
    public void Title()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, "TitleScene");
    }
    //最初からやり直す
    public void Retry()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, SceneManager.GetActiveScene().name);

    }
    //戻る
    public void Back()
    {
        Time.timeScale = 1.0f;
    }
}
