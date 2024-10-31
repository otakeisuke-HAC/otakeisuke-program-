using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStandbyController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameScreenImage;
    [SerializeField]
    private GameObject m_GameExpianationImage;
    private float m_ImageChangeCount = 0;
    private bool m_Change = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ImageChangeCount >= 5)
        {
            m_Change = true;
        }
        else if (m_ImageChangeCount <= 0)
        {
            m_Change = false;
        }

        if(m_Change == true)
        {
            m_ImageChangeCount -= Time.deltaTime;
            m_GameScreenImage.SetActive(false);
            m_GameExpianationImage.SetActive(true);
        }
        else
        {
            m_ImageChangeCount += Time.deltaTime;
            m_GameScreenImage.SetActive(true);
            m_GameExpianationImage.SetActive(false);
        }
    }
}
