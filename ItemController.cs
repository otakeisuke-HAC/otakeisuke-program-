using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject[] m_SkillItemObj;�@//Skill�I�u�W�F�N�g���擾����
    //�X�L����ł��߂̃A�C�e��
    public void SkillItemReset()
    {
        foreach (var item in m_SkillItemObj)
        {
            item.SetActive(true);
        }
    }
}
