using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnClickStartButton()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("Main");

    }
    

    private void GameSceneLoaded(Scene next,LoadSceneMode mode)
    {

    }
}
