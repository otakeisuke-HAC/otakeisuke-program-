using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    SceneFadeController m_SceneFadeController; 
    private Button m_StartButton;

    // Start is called before the first frame update
    void Start()
    {
        m_SceneFadeController = GetComponent<SceneFadeController>();
        Cursor.visible = false;
        m_StartButton = GameObject.Find("StartButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            m_StartButton.Select();
        }
    }

    public void Explanation()
    {
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, "ExplanationScene");
    }

    public void VolumeOption()
    {
        m_SceneFadeController.fadeOutStart(0, 0, 0, 0, "GameAudioControlScene");
    }

    public void OnEnd()
    {
        Application.Quit();
    }
}
