using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject[] m_SkillItemObj;　//Skillオブジェクトを取得する
    //スキルを打つためのアイテム
    public void SkillItemReset()
    {
        foreach (var item in m_SkillItemObj)
        {
            item.SetActive(true);
        }
    }
}
