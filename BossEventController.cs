using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EventWall;
    [SerializeField]
    private GameObject m_BossObj;
    [SerializeField]
    private GameObject m_ClearObj;
    [SerializeField]
    private GameObject m_GollParticle;

    // Update is called once per frame
    void Update()
    {
        if(m_EventWall.activeSelf == true)
        {           
            if(m_EventWall.transform.position.y >= 54.1f)
            {
                m_EventWall.transform.Translate(0, 0, 0);
            }
            else
            {
                m_EventWall.transform.Translate(0, 0.5f, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_EventWall.SetActive(true);
            m_BossObj.SetActive(true);
            GameObject Particle = Instantiate(m_GollParticle, m_ClearObj.transform.position, Quaternion.identity);
            Destroy(Particle,3f);
        }
    }
}
