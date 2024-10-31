using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearSceneController : MonoBehaviour
{
    private SceneFadeController f_GameController;
    private Button m_TitleButton;
    // Start is called before the first frame update
    void Start()
    {
        f_GameController = gameObject.GetComponent<SceneFadeController>();
        m_TitleButton = GameObject.Find("TitleButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            m_TitleButton.Select();
        }
    }


}
