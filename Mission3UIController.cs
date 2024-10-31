using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission3UIController : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    public GameObject[] m_SkeletonObj;
    private int m_SkeletonCount = 0; //�X�P���g����|������
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshProUGUI.text = "���X�P���g����|��(" + m_SkeletonCount + "/3)";
    }

    public void SkeletonDieCount()
    {
        m_SkeletonCount = m_SkeletonCount + 1;
    }
}
