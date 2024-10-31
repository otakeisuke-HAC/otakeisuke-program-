using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionController : MonoBehaviour
{   
    public GameObject[] GateObj; //ゲート
    
    public GameObject[] MissionText; //ミッションテキスト
    public GameObject[] Mission1_Slime; //ミッション用スライム
    public LeverController[] LeverControllers;　//レバー

    public GameObject BossObj; //Bossオブジェクト
    public GameObject GameClearObj; //GameClearオブジェクト
    public GameObject m_Mission3Obj; //Mission3Objオブジェクト
    public GameObject m_FinalObj; //FinalObjオブジェクト
    enum Mission
    {
        Mission0,
        Mission1,
        Mission2,
        Mission3,
        Mission4,
        FinalMission
    }Mission nowMission;

    // Start is called before the first frame update
    void Start()
    {
        nowMission = Mission.Mission0;
        //ミニマップに映るオブジェクトをそのステージのミッションが車で表示しないようにする
        m_Mission3Obj.SetActive(false); 
        m_FinalObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ミッションをクリアしたら次のミッションに進む
        if(nowMission == Mission.Mission0)
        {
            MissionText[0].SetActive(true);
            if (LeverControllers[0].m_LeverSwitch == true)
            {
                GateObj[0].GetComponent<GateController>().m_GateFlag = true;
                MissionText[0].SetActive(false);
                nowMission = Mission.Mission1;
            }
        }
        else if (nowMission == Mission.Mission1)
        {
            Mission1_Slime = GameObject.FindGameObjectsWithTag("Mission1Slime");
            MissionText[1].SetActive(true);
            if (Mission1_Slime.Length <= 0)
            {
                GateObj[1].GetComponent<GateController>().m_GateFlag = true;
                MissionText[1].SetActive(false);
                nowMission = Mission.Mission2;
            }
        }
        else if (nowMission == Mission.Mission2)
        {
            MissionText[2].SetActive(true);
            if (LeverControllers[1].m_LeverSwitch == true && LeverControllers[2].m_LeverSwitch == true 
                && LeverControllers[3].m_LeverSwitch == true)
            {
                MissionText[2].SetActive(false);
                GateObj[2].GetComponent<GateController>().m_GateFlag = true;
                nowMission = Mission.Mission3;
            }
        }
        else if(nowMission == Mission.Mission3)
        {
            m_Mission3Obj.SetActive(true);
            MissionText[3].SetActive(true);
            GameObject Mission3StairsControllerObj = GameObject.Find("Mission3Stairs");
            if (Mission3StairsControllerObj.GetComponent<Mission3StairsController>().Mission3Flag == true)
            {
                MissionText[3].SetActive(false);
                nowMission = Mission.Mission4;
            }
        }
        else if(nowMission == Mission.Mission4)
        {
            m_FinalObj.SetActive(true);
            MissionText[4].SetActive(true);
            if(BossObj.activeSelf == true)
            {
                nowMission = Mission.FinalMission;
                MissionText[4].SetActive(false);
                GameClearObj.SetActive(false);
            }
        }
        else if (nowMission == Mission.FinalMission)
        {            
            MissionText[5].SetActive(true);
            if(BossObj == null)
            {
                GameClearObj.SetActive(true);
                MissionText[6].SetActive(true);
            }
        }
    }
}
