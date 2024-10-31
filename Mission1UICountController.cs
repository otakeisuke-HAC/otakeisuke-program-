using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission1UICountController : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    public GameObject[] m_SlimeObj;
    private int m_Slime = 5;
    private int m_SlimeCount;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        m_SlimeObj = GameObject.FindGameObjectsWithTag("Mission1Slime");
        if (m_SlimeObj.Length < m_Slime)
        {
            m_Slime = m_SlimeObj.Length;
            m_SlimeCount++;
        }

        m_TextMeshProUGUI.text = "™‘S‚Ä‚ÌƒXƒ‰ƒCƒ€‚ð“|‚¹(" + m_SlimeCount + "/5)";
    }
}
