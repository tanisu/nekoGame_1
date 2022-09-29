using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public void Retry()
    {
        _pushSE();
        SceneMove.instance.isRetry = true;
        SceneMove.instance.SceneTransition(1,1);
    }

    public void DefaultMove()
    {
        _pushSE();
        SceneMove.instance.SceneTransition(0, 1);
    }

    public void ToTitle()
    {
        _pushSE();
        SceneMove.instance.isGameStart = false;
        SceneMove.instance.SceneTransition(1, 0);
    }

    private void _pushSE()
    {
        SoundController.I.PlaySE(SESoundData.SE.BUTTON);
    }

    public void SelectScene(int _next)
    {
        _pushSE();
        SceneMove.instance.SceneTransition(SceneMove.instance.GetCurrrentSceneIndex(),_next);
    }
}
