using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private Image[] m_LifeImage;
    [SerializeField]
    private PlayerController Player;
    [SerializeField]
    private int m_PlayerHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeUI();
    }

    void LifeUI()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        m_PlayerHp = Player.m_HP;
        for (int i = m_PlayerHp; i < m_LifeImage.Length; i++)
        {
            m_LifeImage[i].enabled = false;
        }

    }
}
