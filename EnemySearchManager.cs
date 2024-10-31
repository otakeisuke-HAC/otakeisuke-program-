using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject s_PlayerObj;�@//�v���C���[�I�u�W�F�N�g
    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[�I�u�W�F�N�g���@�m�G���A�ɓ�������擾
        if (other.CompareTag("Player"))
        {
            s_PlayerObj = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //�v���C���[�I�u�W�F�N�g���@�m�G���A����o���疳��
        if (other.CompareTag("Player"))
        {
            s_PlayerObj = null;
        }
    }

}
