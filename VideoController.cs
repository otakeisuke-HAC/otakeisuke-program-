using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    private NextSceneController m_NextSceneController;
    private VideoPlayer m_VideoPlayer;
    private Button m_NextButton;

    //ビデオ
    //順番 Walk→Jump→Attack→Block→SkillItem→Skill→ViewpointMove
    //      0      1     2       3        4        5         6
    public VideoClip[] m_NextVideos;
    public int m_NextVideoCount = 0;
    public int m_BackVideoCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_NextSceneController = GetComponent<NextSceneController>();
        m_VideoPlayer = GetComponent<VideoPlayer>();
        m_VideoPlayer.clip = m_NextVideos[0];
        m_NextButton = GameObject.Find("NextButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(m_VideoPlayer.clip == m_NextVideos[m_NextVideoCount])
        {
            m_NextVideoCount++;            
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            m_NextButton.Select();
        }
    }
    //次のビデオに切り替える
    public void NextVideo()
    {
        if(m_NextVideoCount == m_NextVideos.Length)
        {
            m_NextSceneController.NextScene();
            m_NextButton.enabled = false;
        }
        else
        {
            m_VideoPlayer.clip = m_NextVideos[m_NextVideoCount];
        }

    }

    public void ResetVideo()
    {
        m_NextVideoCount = 0;
        m_VideoPlayer.clip = m_NextVideos[0];
    }
}
