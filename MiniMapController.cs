using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private Transform m_Player; //�v���C���[��Transforms
    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̃I�u�W�F�N�g���擾
        m_Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�~�j�J�����Ńv���C���[���f�����߂̏���
        gameObject.transform.position = new Vector3(m_Player.transform.position.x,m_Player.transform.position.y + 30,m_Player.transform.position.z);
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
