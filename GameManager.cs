using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IEnumerator Inoperable(float i) // �����s�\�ɂ���i�����̕b���ԁj
    {
        GameObject inputObj = GameObject.Find("Player");
        PlayerController playerController = inputObj.GetComponent<PlayerController>();
        playerController.enabled = false; // �X�N���v�g�𖳌���
        yield return new WaitForSeconds(i); // �����̕b�������҂�
        playerController.enabled = true; // �X�N���v�g��L����
        yield break;
    }

    public void CallInoperable(float i)
    {
        StartCoroutine("Inoperable", i); // ���̃X�N���v�g����Ăяo���p
    }
}