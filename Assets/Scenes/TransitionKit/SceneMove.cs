using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SceneMove : MonoBehaviour
{
    // 使いたいテクスチャ
    public Texture2D maskTexture;
    public bool isGameStart,isRetry;
    public UnityAction StageChange;
    public int currentStage;

    // デフォルトはBlack
    [SerializeField] Color _bgColor;

    #region シングルトン化
    public static SceneMove instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    

    public void SceneTransition(int now, int next)
    {
        //isGameStart = now == 1 ? true : false;
        
        ImageMaskTransition mask = new ImageMaskTransition()
        {
            maskTexture = maskTexture,
            backgroundColor = _bgColor,

            // ?が if左==右だったらの役割、trueは：の左、falseが右の結果
            nextScene = SceneManager.GetActiveScene().buildIndex == now ? next : now
        };
        TransitionKit.instance.transitionWithDelegate(mask);
    }

    public void StageClear()
    {
        
        ImageMaskTransition mask = new ImageMaskTransition()
        {
            maskTexture = maskTexture,
            backgroundColor = _bgColor,
        };
        TransitionKit.instance.transitionWithDelegate(mask);
    }
}
