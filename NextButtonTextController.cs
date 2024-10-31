using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextButtonTextController : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    public VideoController m_VideoController;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_VideoController = GameObject.Find("VideoController").GetComponent<VideoController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_VideoController.m_NextVideoCount == m_VideoController.m_NextVideos.Length)
        {
            m_TextMeshProUGUI.text = "ƒ^ƒCƒgƒ‹‚Ö";
        }
        else 
        {
            m_TextMeshProUGUI.text = "ŽŸ‚Ö";
        }
    }

}
