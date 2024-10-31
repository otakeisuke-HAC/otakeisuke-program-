using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject s_PlayerObj;　//プレイヤーオブジェクト
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーオブジェクトが察知エリアに入ったら取得
        if (other.CompareTag("Player"))
        {
            s_PlayerObj = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //プレイヤーオブジェクトが察知エリアから出たら無効
        if (other.CompareTag("Player"))
        {
            s_PlayerObj = null;
        }
    }

}
