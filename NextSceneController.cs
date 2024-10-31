using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneController : MonoBehaviour
{
    public string m_NextSceneName; //éüÇÃÉVÅ[ÉìÇÃñºëO
    private SceneFadeController f_NextSceneController;
    // Start is called before the first frame update
    void Start()
    {
        f_NextSceneController = GetComponent<SceneFadeController>();
    }
    public void NextScene()
    {
        f_NextSceneController.fadeOutStart(0, 0, 0, 0, m_NextSceneName);
    }
}
